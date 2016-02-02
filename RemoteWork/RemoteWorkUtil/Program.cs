using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteWork.Access;
using RemoteWork.Data;
using System.Data.Entity;
using RemoteWork.Expect;
using System.Threading;

namespace RemoteWorkUtil
{
    class Program
    {
        static RconfigContext context;
        static void Main(string[] args)
        {
            
            //ConnectionData data = new ConnectionData();
            //data.address = "192.168.234.130";
            //data.password = "Zx_998877Kad";
            //data.port = 22;
            //data.username = "root";
            //SshExpect ssh = new SshExpect(data);
            //ssh.ExecuteCommand("ls -la");
            //if (ssh.isSuccess)
            //{
            //    Console.WriteLine(ssh.GetResult());

            //}
            //else
            //{
            //    Console.WriteLine(ssh.GetError());
                
            //}
            //Console.ReadKey();


            //**
            //int TaskID = 4;
            //context = new RconfigContext();
            //var queryTask=(from c in context.RemoteTasks
            //              where c.Id==TaskID

            //              select c).FirstOrDefault();

            //if (queryTask != null)
            //{
            //    LoadConfiguration(queryTask);
            //}
            //Console.ReadKey();
        }

        private static void LoadConfiguration(RemoteTask task)
        {
            CancellationToken token;

            List<Task> taskRunnerManager = new List<Task>();
            //LockForm();
            var tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            //   ProgressInit(task.Favorites.Count);
            try
            {
                foreach (Favorite fav in task.Favorites)
                {
                    //STARTED
                    Console.WriteLine("OPERATION started for {0}...", fav.Address);
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

                    Task taskRunner = Task.Factory.StartNew(() =>
                        Connection(connect, token), token
                    );
                    /*
                     * t = Task.Factory.StartNew(() => DoSomeWork(1, token), token);
            Console.WriteLine("Task {0} executing", t.Id);
            tasks.Add(t);
                     */
                    Console.WriteLine("Task {0} started...", taskRunner.Id);
                    taskRunnerManager.Add(taskRunner);
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

            //LockForm();
            // NotifyInfo("Success!");
            //  ProgressClear();
        }
        //проблема с многопоточностью
        //наверняка нужно создавать новый контекст для каждого потока
        //или опрашивать и сохранять данные локально с последующей выгрузкой в базу данных
        private static void Connection(FavoriteConnect favConnect, CancellationToken ct)
        {
            //token stopped!!
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Task was cancelled and not started!");
                ct.ThrowIfCancellationRequested();
            }

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
            Expect expect;
            switch (protocol)
            {
                case "Telnet": expect = new TelnetExpect(data); break;
                case "SSH": expect = new SshExpect(data); break;
                //по умолчанию для сетевых устройств протокол Telnet
                default: expect = new TelnetExpect(data); break;
            }

            //token stopped!!
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Task was cancelled!");
                ct.ThrowIfCancellationRequested();
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
                    config.Date = DateTime.UtcNow;
                    fav.Configs.Add(config);
                }
                //создаем отчет о проделанном задании
                Report report = new Report();
                report.Date = DateTime.UtcNow;
                report.Status = success;
                report.Info = error;
                report.Task = task;
                report.Favorite = fav;
                context.Reports.Add(report);
                context.SaveChanges();
            }
        }
        private void Test()
        {
            Database.SetInitializer(new Init());
            //  FirstInsert();
            //SecondInsert();
            using (RconfigContext ctx = new RconfigContext())
            {
                Console.WriteLine(ctx.Categories.Count());
                foreach (Category cat in ctx.Categories)
                {
                    Console.WriteLine(cat.CategoryName);
                }
                Console.WriteLine(ctx.Protocols.Count());
                foreach (Protocol prot in ctx.Protocols)
                {
                    Console.WriteLine("Protocol Name: {0} DefPort: {1}", prot.Name, prot.DefaultPort);
                }

                foreach (Location loc in ctx.Locations)
                {

                    Console.WriteLine("LOC: {0}", loc.LocationName);
                }
                foreach (Favorite fav in ctx.Favorites)
                {
                    Console.WriteLine("Fav:{0}, Address: {1}", fav.Hostname, fav.Address);
                }
            }
            Console.WriteLine("CHECKED");
            Console.ReadKey();
        }
        static void FirstInsert()
        {
            using (RconfigContext context = new RconfigContext())
            {
                //PROTOCOL
                context.Protocols.Add(new Protocol
                {
                    Name = "SSH",
                    DefaultPort = 22
                });
                context.Protocols.Add(new Protocol
                {
                    Name = "Telnet",
                    DefaultPort = 23
                });

                //Categories
                Category routers = new Category
                {
                    CategoryName = "Routers"
                };

                Category switches = new Category
                {
                    CategoryName = "Switches"
                };

                Category servers = new Category
                {
                    CategoryName = "Servers"
                };

                context.Categories.Add(routers);
                context.Categories.Add(switches);
                context.Categories.Add(servers);

                //COMMANDS
                ICollection<Category> cisco = new HashSet<Category>();
                cisco.Add(routers);
                cisco.Add(switches);

                context.Commands.Add(new Command
                {
                    Name = "show running-config",
                    Categories = cisco
                });
                ICollection<Category> vlan = new HashSet<Category>();
                vlan.Add(switches);
                context.Commands.Add(new Command
                {
                    Name = "show ip vlan brief",
                    Categories = vlan
                });

                //CREDENTIALS
                context.Credentials.Add(new Credential
                {
                    CredentialName = "Default",
                    Username = "root",
                    Domain = "domain.com",
                    Password = "toor"
                });

                context.SaveChanges();
            }
        }
        static void SecondInsert()
        {
            using (RconfigContext context = new RconfigContext())
            {
                context.Locations.Add(new Location
                {
                    LocationName="Syslocation"
                });
                context.SaveChanges();
            }
        }
    }


}
