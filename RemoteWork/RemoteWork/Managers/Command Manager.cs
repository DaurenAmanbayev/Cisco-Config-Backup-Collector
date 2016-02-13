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

namespace RemoteWork.Managers
{
    //добавить дополнительные колонки для описания программного обеспечения
    public partial class Command_Manager : Form
    {
        RconfigContext context;

        public Command_Manager()
        {
            InitializeComponent();
            LoadData();
        }
        //подгружаем данные
        private async void LoadData()
        {
            using (context = new RconfigContext())
            {
                var queryCommands = await (from c in context.Commands
                                           select c).ToListAsync();

                if (queryCommands != null)
                {
                    listViewCommands.Items.Clear();
                    foreach (Command command in queryCommands)
                    {
                        var item = new ListViewItem(new[] {
                        command.Name,
                        command.Order.ToString()
                    });
                        listViewCommands.Items.Add(item);
                    }
                }
            }
        }
        //добавить команду
        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_Edit frm = new Command_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }
        //редактировать команду
        private void editCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewCommands.SelectedItems.Count != 0)
            {
                var item = listViewCommands.SelectedItems[0];
                string command = item.SubItems[0].Text;
                Command_Edit frm = new Command_Edit(command);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        //удалить команду
        private void deleteCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewCommands.SelectedItems.Count != 0)
            {
                using (context = new RconfigContext())
                {
                    var item = listViewCommands.SelectedItems[0];
                    string command = item.SubItems[0].Text;
                    // MessageBox.Show(listBoxCommands.SelectedItem.ToString());
                    var queryCommand = (from c in context.Commands
                                        where c.Name == command
                                        select c).FirstOrDefault();
                    if (queryCommand != null)
                    {
                        context.Commands.Remove(queryCommand);
                        context.SaveChanges();
                        LoadData();
                    }
                }
            }
        }
    }
}
