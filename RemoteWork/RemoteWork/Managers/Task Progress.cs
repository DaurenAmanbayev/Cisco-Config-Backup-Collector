using RemoteWork.Access;
using RemoteWork.Data;
using RemoteWork.Expect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Threading;

namespace RemoteWork.Managers
{
    public partial class Task_Progress : Form
    {
        RconfigContext context;
        int max = 0;
        int current = 0;
        int TaskID;
        public Task_Progress(int TaskID)
        {
            InitializeComponent();
            this.TaskID = TaskID;
           
        }
        //инициализация прогресс бара
        private void ProgressInit()
        {
            progressBarTask.Maximum = max;
            progressBarTask.Minimum = 0;
            progressBarTask.Step = 1;
        }
        //шаг прогресс бара
        private void ProgressStep(string info)
        {
            labelStatus.Text = string.Format("Load for: {0} | Progress: {1} of {2}", info, current, max);//проблема с отображением прогресса
            progressBarTask.PerformStep();
        }
        //очистка прогресс бара
        private void ProgressFinish()
        {
            progressBarTask.Value = 0;
            this.DialogResult = DialogResult.OK;
        }
        //запуск прогресс бара
        private void ProgressStart()
        {
            LoopMethod(TaskID);
        }
        //обход и сбор конфигурации с устройств по задаче
        #region LOOP USE
        //проход в цикле для каждого устройства
        private async void LoopMethod(int TaskID)
        {
            using (context = new RconfigContext())
            {
                var queryTask = await (from c in context.RemoteTasks
                                 where c.Id == TaskID

                                 select c).FirstOrDefaultAsync();
                if (queryTask != null)
                {
                    max = queryTask.Favorites.Count;
                    LoadConfiguration(queryTask);
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }
        /*        
         * The entity framework DbContext and ObjectContext classes are NOT thread-safe. So you should not use them over multiple threads.       
         * Although it seems like you're only passing entities to other threads, it's easy to go wrong at this, when lazy loading is involved.        
         * This means that under the covers the entity will callback to the context to get some more data.        
         * So instead, I would advice to convert the list of entities to a list of special immutable data structures that only need the data that is needed for the calculation.
         * Those immutable structures should not have to call back into the context and should not be able to change. 
         * When you do this, it will be safe to pass them on to other threads to do the calculation.
         */
        private void LoadConfiguration(RemoteTask task)
        {            
            ProgressInit();
            try
            {
                foreach (Favorite fav in task.Favorites)
                {
                    current++;
                    ProgressStep(fav.Address);         
                    List<string> commands = new List<string>();
                    //проходим по списку команд, выявляем соответствие используемой команды и категории избранного               
                    foreach (Command command in task.Commands)
                    {
                        foreach (Category category in command.Categories)
                        {
                            if (fav.Category.CategoryName == category.CategoryName)
                            {
                                commands.Add(command.Name);
                            }
                        }
                    }
                    //мультипоточность
                    //устанавливаем соединение
                    FavoriteConnect connect = new FavoriteConnect();
                    connect.commands = commands;
                    connect.favorite = fav;
                    connect.task = task;
                    Connection(connect);
                }
                MessageBox.Show("Task is complete!", "Task Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProgressFinish();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //LockForm();
            // NotifyInfo("Success!");
            //  ProgressClear();
        }
        //проблема с многопоточностью
        //наверняка нужно создавать новый контекст для каждого потока
        //или опрашивать и сохранять данные локально с последующей выгрузкой в базу данных
        private void Connection(FavoriteConnect favConnect)
        {

            RemoteTask task = favConnect.task;
            List<string> commands = favConnect.commands;
            Favorite fav = favConnect.favorite;
            //данные для подключения к сетевому устройству
            ConnectionData data = new ConnectionData();
            data.address = fav.Address;
            data.port = fav.Port;
            data.username = fav.Credential.Username;
            data.password = fav.Credential.Password;

            //по типу протоколу выбираем требуемое подключение
            string protocol = fav.Protocol.Name;
            Expect.Expect expect;
            switch (protocol)
            {
                case "Telnet": expect = new TelnetMintExpect(data); break;
                case "SSH": expect = new SshExpect(data); break;
                //по умолчанию для сетевых устройств протокол Telnet
                default: expect = new TelnetMintExpect(data); break;
            }

            //если объект expect успешно создан
            if (expect != null)
            {
                //выполняем список команд
                expect.ExecuteCommands(commands);
                string result = expect.GetResult();
                bool success = expect.isSuccess;
                string error = expect.GetError();
                //если успешно сохраняем конфигурацию устройства
                if (success)
                {
                    Config config = new Config();
                    config.Current = result;
                    config.Date = DateTime.Now;
                    fav.Configs.Add(config);
                }
                //создаем отчет о проделанном задании
                Report report = new Report();
                report.Date = DateTime.Now;
                report.Status = success;
                report.Info = error;
                report.Task = task;
                report.Favorite = fav;
                context.Reports.Add(report);
                context.SaveChanges();
            }
        }
        #endregion
        //как только форма отрисовалась, запустить операцию и прогресс бар
        private void Task_Progress_Shown(object sender, EventArgs e)
        {
            ProgressStart();
        }

    }

}
