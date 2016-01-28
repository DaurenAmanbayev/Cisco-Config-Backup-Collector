using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteWork.Access;
using RemoteWork.Data;
using System.Data.Entity;

namespace RemoteWorkUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new Init());
           // Test();
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
    }


}
