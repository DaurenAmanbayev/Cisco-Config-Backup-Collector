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
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RemoteWorkUtil
{
    class Program
    {
        static RconfigContext context;
        static void Main(string[] args)
        {
            //*ERROR**
            int TaskID = 1;
            context = new RconfigContext();
            var queryTask = (from c in context.RemoteTasks
                             where c.Id == TaskID

                             select c).FirstOrDefault();
            Console.WriteLine("Process started...");
            if (queryTask != null)
            {
                LoadConfiguration(queryTask);
                Console.WriteLine("Finish!");
            }
            else
            {
                Console.WriteLine("Task is null!");
            }
            Console.ReadKey();
        }
        /*        
         * The entity framework DbContext and ObjectContext classes are NOT thread-safe. So you should not use them over multiple threads.       
         * Although it seems like you're only passing entities to other threads, it's easy to go wrong at this, when lazy loading is involved.        
         * This means that under the covers the entity will callback to the context to get some more data.        
         * So instead, I would advice to convert the list of entities to a list of special immutable data structures that only need the data that is needed for the calculation.
         * Those immutable structures should not have to call back into the context and should not be able to change. 
         * When you do this, it will be safe to pass them on to other threads to do the calculation.
         */
        private static void LoadConfiguration(RemoteTask task)
        {
            CancellationToken token;

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

                    Console.WriteLine("Connection started for {0} favorites...", countFav++);
                    Connection(connect, token);
                    //Многопоточный вариант отпадает в связи с ограничением работы EF с потоками
                    //
                    //Task taskRunner = Task.Factory.StartNew(() =>
                    //    Connection(connect, token), token
                    //);
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
                //foreach (Task taskRunner in taskRunnerManager)
                //{
                //    // ProgressStep();
                //    taskRunner.Wait();
                //    Console.WriteLine("Task {0} finished...", taskRunner.Id);
                //}
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
            //if (ct.IsCancellationRequested)
            //{
            //    Console.WriteLine("Task was cancelled and not started!");
            //    ct.ThrowIfCancellationRequested();
            //}

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

        #region TEST METHODS
        //тестовые методы
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
        #endregion
    }
    //deep cloning object 
    //object must be serializable
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }

}
