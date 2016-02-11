using RemoteWork.Access;
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
using RemoteWork.Data;
using RemoteWork.Expect;

namespace RemoteWork
{
    public partial class Load_Config : Form
    {
        string favName;
        string category;
      
        public Load_Config(string favName, string category)
        {
            InitializeComponent();
            this.favName = favName;
            this.category = category;
            labelFav.Text = favName;
            LoadData();
        }
        //подгрузка данных
        private async void LoadData()
        {
            using(RconfigContext context=new RconfigContext())
            {
                var queryCategory=(from c in context.Categories
                                  where c.CategoryName==category
                                  select c).Single();
                if (queryCategory != null)
                {
                    var queryCommands = await (from c in context.Commands                                      
                                        select c).ToListAsync();
                    //пробегаемся по списку команд отсортированну
                    foreach (Command command in queryCommands.OrderBy(c=>c.Order))
                    {
                        foreach (Category category in command.Categories)
                        {
                            if (category.CategoryName == this.category)
                            {
                                checkedListBoxCommand.Items.Add(command.Name);
                            }
                        }
                    }                    
                }
            }
        }
        //запуск сбора конфигурации
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (checkedListBoxCommand.CheckedItems.Count != 0)
            {
                DialogResult result = MessageBox.Show("This load configuration will not be saved to database!", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                //предупредить пользователя, что единовременный сбор конфигурации не будет храниться в базе данных
                //затем сделать сбор конфигурации
                if (result == DialogResult.OK)
                {
                    //создаем список команд
                    List<string> commands = new List<string>();
                    foreach (var item in checkedListBoxCommand.CheckedItems)
                    {
                        string command = item.ToString();
                        commands.Add(command);
                    }
                    //передаем наш список для сбора конфигурации
                    Connection(commands);
                }
            }
            else
            {
                MessageBox.Show("Must be checked configuration commands!", "Load Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //подключение к сетевому устройству
        private void Connection(List<string> commands)
        {
            using (RconfigContext context = new RconfigContext())
            {
                var fav=(from c in context.Favorites
                        where c.Hostname==favName
                        select c).Single();
                if (fav != null)
                {
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
                            richTextBoxConfig.Text = result;
                        }
                        else
                        {
                            richTextBoxConfig.Text = error;
                        }
                    }
                }
            }
        }
    }
}
