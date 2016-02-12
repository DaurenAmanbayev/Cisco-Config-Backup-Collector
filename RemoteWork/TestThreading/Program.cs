using RemoteWork.Access;
using RemoteWork.Data;
using RemoteWork.Expect;
using RemoteWorkUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskUse();
        }
        //многопоточный вариант
        //не реализован!!!!
        #region TASK USE
        private static void TaskUse()
        {
            int taskID = 5;
            DateTime start = DateTime.Now;
            LoadConfigurationThread(taskID);
            DateTime end = DateTime.Now;
            TimeSpan diff = start - end;
            Console.WriteLine("Finish"+diff);
            Console.ReadKey();
            
        }
        //для подгрузки данных использовать ADO.NET и пул соединений
        private static void LoadConfigurationThread(int taskId)
        {
            CancellationToken token;
            using (RconfigContext context = new RconfigContext())
            {
                var task = (from c in context.RemoteTasks
                            where c.Id == taskId
                            select c).FirstOrDefault();

                if (task != null)
                {
                    List<Task> taskRunnerManager = new List<Task>();
                    //LockForm();
                    var tokenSource = new CancellationTokenSource();
                    token = tokenSource.Token;
                    int countFav = 0;
                    //   ProgressInit(task.Favorites.Count);
                    try
                    {
                        foreach (Favorite fav in task.Favorites)
                        {
                            //STARTED
                            Console.WriteLine("OPERATION started for {0}...", fav.Address);
                            List<string> commands = new List<string>();
                            //проходим по списку команд, выявляем соответствие используемой команды и категории избранного               
                            foreach (Command command in task.Commands.OrderBy(c => c.Order))
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
                            FavoriteTask connect = new FavoriteTask();
                            connect.Commands = commands;
                            connect.FavoriteId = fav.Id;
                            connect.TaskId = task.Id;

                            //FavoriteConnect connect = new FavoriteConnect();
                            //connect.commands = commands;
                            //connect.favorite = fav;
                            //connect.task = task;

                            Console.WriteLine("Connection started for {0} favorites...", countFav++);
                            // ConnectionThread(connect, token);
                            //Многопоточный вариант отпадает в связи с ограничением работы EF с потоками

                            Task taskRunner = Task.Factory.StartNew(() =>
                                ConnectionThread(connect));
                            /*
                             t = Task.Factory.StartNew(() => DoSomeWork(1, token), token)
                             Console.WriteLine("Task {0} executing", t.Id);
                             tasks.Add(t);
                             */
                            //Console.WriteLine("Task {0} started...", taskRunner.Id);
                            // taskRunnerManager.Add(taskRunner);
                            //Connection(fav, commands, task);
                        }

                        //дожидаемся пока выполняться все задания
                        foreach (Task taskRunner in taskRunnerManager)
                        {
                            // ProgressStep();
                            taskRunner.Wait();
                            Console.WriteLine("Task {0} finished...", taskRunner.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //LockForm();
            // NotifyInfo("Success!");
            //  ProgressClear();
        }
        //проблема с многопоточностью
        //наверняка нужно создавать новый контекст для каждого потока
        //или опрашивать и сохранять данные локально с последующей выгрузкой в базу данных
        private static void ConnectionThread(FavoriteTask favConnect)
        {
            //Console.WriteLine("REPORT!!");
            
            //token stopped!!
            //if (ct.IsCancellationRequested)
            //{
            //    Console.WriteLine("Task was cancelled and not started!");
            //    ct.ThrowIfCancellationRequested();
            //}
           
            using (RconfigContext ctx = new RconfigContext())
            {
                var task = (from c in ctx.RemoteTasks
                            where c.Id == favConnect.TaskId
                            select c).Single();

                List<string> commands = favConnect.Commands;

                Favorite fav = (from c in ctx.Favorites
                                where c.Id == favConnect.FavoriteId
                                select c).Single();

              
                //данные для подключения к сетевому устройству
                ConnectionData data = new ConnectionData();
                data.address = fav.Address;
                data.port = fav.Port;
                data.username = fav.Credential.Username;
                data.password = fav.Credential.Password;

                //по типу протоколу выбираем требуемое подключение
                string protocol = fav.Protocol.Name;
                Expect expect;
                switch (protocol)
                {
                    case "Telnet": expect = new TelnetMintExpect(data); break;
                    case "SSH": expect = new SshExpect(data); break;
                    //по умолчанию для сетевых устройств протокол Telnet
                    default: expect = new TelnetMintExpect(data); break;
                }

                //token stopped!!
                //if (ct.IsCancellationRequested)
                //{
                //    Console.WriteLine("Task was cancelled!");
                //    ct.ThrowIfCancellationRequested();
                //}

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
                    ctx.Reports.Add(report);
                    ctx.SaveChanges();

                   // Console.WriteLine("REPORT!!");
                }
            }

        }
        #endregion


        #region ADO.NET multihreading
        private static void Manager()
        {
 
        }
        private static void Connect()
        {
 
        }
        #endregion
    }
}
