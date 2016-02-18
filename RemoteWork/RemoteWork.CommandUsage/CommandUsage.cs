using RemoteWork.Access;
using RemoteWork.Data;
using RemoteWork.Expect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteWork.CommandUsage
{
    public class CommandUsage
    {
        int taskId;
        string logJournal = "rconfig-journal.log";
        CommandUsageMode mode=CommandUsageMode.LoopUsage;
        RconfigContext context;        
        public CommandUsage(int taskId, CommandUsageMode mode)
        {
            this.taskId = taskId;
            this.mode = mode;
        }
        //для уведмоления о том, что все запущенные задачи выполнены
        public delegate void TaskReport();
        public event TaskReport taskCompleted;
        //основной метод для вызова выполнения задачи
        public void Dispatcher()
        {
            switch (mode)
            {
                case CommandUsageMode.LoopUsage: LoopMethod(taskId); break;
                case CommandUsageMode.TaskParallelUsage: LoadConfigurationThread(taskId); break;
            }
        }
        //проверка валидности идентификатора задачи
        private bool CheckTask(int taskId)
        {
            using (RconfigContext ctx = new RconfigContext())
            {
                var queryTask = (from c in ctx.RemoteTasks
                                 where c.Id == taskId
                                 select c).Single();
                if (queryTask != null)
                    return true;
                else
                    return false;
            }
        }
        //многопоточный вариант обхода и сбора конфигураций с устройств по задаче
        #region TASK PARALLEL USAGE
        //возможен конфликт при параллельной вставке данных
        //требуется протестировать
        private void LoadConfigurationThread(int taskId)
        {
            using (RconfigContext context = new RconfigContext())
            {
                var task = (from c in context.RemoteTasks
                            where c.Id == taskId
                            select c).FirstOrDefault();

                if (task != null)
                {
                    List<Task> taskRunnerManager = new List<Task>();        
                    try
                    {
                        Logging(string.Format("TASK {0} started... THREADING", taskId));
                        //если количество избранных равна нулю коэффициент ожидания равен 1
                        //int waiter = (task.Favorites.Count>0) ? task.Favorites.Count : 1;
                        foreach (Favorite fav in task.Favorites)
                        {                            
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

                            //создаем задачу и добавляем ее в менеджер
                            Task taskRunner = Task.Factory.StartNew(() =>
                                ConnectionThread(connect));
                            taskRunnerManager.Add(taskRunner);
                        }                        
                        //дожидаемся пока выполняться все задания                       
                        Task.WaitAll(taskRunnerManager.ToArray());
                        //************************************************************
                        //создаем событие и уведомляем о том, что все задачи выполнены
                        taskCompleted();
                    }
                    catch (Exception ex)
                    {
                        //логгирование
                        Logging(string.Format("TASK {0} failed!!! Exception: {1}", taskId, ex.Message));
                    }
                }
                else
                {
                    //записать в логи провал
                    Logging(string.Format("TASK {0} failed!!! Not correct task ID!!!", taskId));
                }
            }
        }
        //проблема с многопоточностью
        //создаем новый контекст для каждого потока
        private void ConnectionThread(FavoriteTask favConnect)
        {          
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
                data.enableMode = fav.Category.EnableModeRequired;
                data.enablePassword = fav.Credential.EnablePassword;
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
                        config.Current = result ?? "Empty";
                        config.Date = DateTime.Now;
                        fav.Configs.Add(config);
                        Logging(string.Format("TASK {0} : success connection for {0} {1}", taskId, fav.Hostname, fav.Address));
                    }
                    else
                    {
                        Logging(string.Format("TASK {0} : failed connection for {0} {1}!!!", taskId, fav.Hostname, fav.Address));
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
                }
            }

        }
        #endregion

        //вариант прохождения в цикле обхода и сбора конфигураций с устройств по задаче
        //проверяется корректность идентификатора задачи
        //затем собираются данные по командам доступные для устройства
        //затем запускается сбор конфигураций
        #region LOOP USAGE
        //метод опроса в цикле
        private void LoopMethod(int taskId)
        {
            context = new RconfigContext();

            var queryTask = (from c in context.RemoteTasks
                             where c.Id == taskId
                             select c).FirstOrDefault();
            //записать в логи операция началась
            if (queryTask != null)
            {
                Logging(string.Format("TASK {0} started... LOOP METHOD ", taskId));
                LoadConfiguration(queryTask);
                //записать в логи успешное завершение
                Logging(string.Format("TASK {0} finished...", taskId));
            }
            else
            {
                //записать в логи провал
                Logging(string.Format("TASK {0} failed!!! Not correct task ID!!!", taskId));
            }
        }
        //сбор данных задачи для исполнения команд
        private void LoadConfiguration(RemoteTask task)
        {
            //  Logging("STARTED");
            foreach (Favorite fav in task.Favorites)
            {
                List<string> commands = new List<string>();
                //проходим по списку команд, выявляем соответствие используемой команды и категории избранного 
                //в сортированном списке по ордеру
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
                //устанавливаем соединение
                FavoriteConnect connect = new FavoriteConnect();
                connect.commands = commands;
                connect.favorite = fav;
                connect.task = task;
                try
                {
                    Connection(connect);
                }
                catch (Exception ex)
                {
                    //записать в логи!!!
                    Logging(string.Format("TASK {0} failed!!! Exception: {1}!!!", taskId, ex.Message));
                }
            }
            //************************************************************
            //создаем событие и уведомляем о том, что все задачи выполнены
            taskCompleted();
        }   
        //исполнение команды для устройства
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
            data.enableMode = fav.Category.EnableModeRequired;           
            data.enablePassword = fav.Credential.EnablePassword;
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
                //если возвращает пустую строку, то выдает исключение
                string result = expect.GetResult();               
                bool success = expect.isSuccess;
                string error =expect.GetError();
                //если успешно сохраняем конфигурацию устройства
                if (success)
                {
                    Config config = new Config();                   
                    config.Current = result ?? "Empty";//если строка пустая, вернуть Empty
                    config.Date = DateTime.Now;
                    fav.Configs.Add(config);
                    Logging(string.Format("TASK {0} : success connection for {0} {1}", taskId, fav.Hostname, fav.Address));
                }
                else
                {
                    Logging(string.Format("TASK {0} : failed connection for {0} {1}!!!", taskId, fav.Hostname, fav.Address));
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

        //логгирование процедуры
        #region LOGGING
        static object lockedObj = new object();
        private void Logging(string log)
        {
            string[] content = new string[1] { "**** Logging data ****" };
            FileInfo fileInf = new FileInfo(logJournal);
            if (fileInf.Exists && fileInf.Length < 4000000)//если размер не превышает 4 Мб, прочитать и дополнить данные лога
            {
                FileRead(logJournal, ref content);
            }
            string buffer = string.Join("\n", content);
            string line = "\n";
            string space = " => ";
            string date = DateTime.Now.ToString();
            WriteCharacters(buffer + line + date + space + log, logJournal);
        }
        private void FileRead(string targetPath, ref string[] content)
        {
            try
            {
                lock (lockedObj)
                {
                    content = File.ReadAllLines(targetPath);
                }
            }
            catch (Exception)
            {

            }
        }
        private async void WriteCharacters(string targetText, string targetPath)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(targetPath))
                {
                    await writer.WriteLineAsync(targetText);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
