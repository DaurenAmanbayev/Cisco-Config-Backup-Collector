using RemoteWork.Access;
using RemoteWork.Data;
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

namespace RemoteWork.Managers
{
    public partial class Task_Manager : Form
    {
        RconfigContext context = new RconfigContext();

        public Task_Manager()
        {
            InitializeComponent();
            LoadData();
            //TaskAddTest();
        }

        private async  void LoadData()
        {
            var queryTasks=await  (from c in context.RemoteTasks
                           select c).ToListAsync();

            if (queryTasks != null)
            {

                foreach (RemoteTask task in queryTasks)
                {
                    var item = new ListViewItem(new [] { 
                    task.Id.ToString(),
                    task.TaskName,
                    task.Description,
                    task.Commands.Count.ToString(),
                    task.Favorites.Count.ToString(),
                    task.Date.ToString()});

                    listViewDetails.Items.Add(item);
                }
            }
        }
        //удалить тестовый метод
        private void TaskAddTest()
        {
            var item = new ListViewItem(new[] { 
                    "1",
                    "LoadConf",
                    "Cherry",
                    "show run",
                    "25",
                    DateTime.UtcNow.ToString()});

            listViewDetails.Items.Add(item);
            var item2 = new ListViewItem(new[] { 
                    "2",
                    "LoadConf",
                    "Cherry",
                    "show run",
                    "25",
                    DateTime.UtcNow.ToString()});
            listViewDetails.Items.Add(item2);
            var item3 = new ListViewItem(new[] { 
                    "3",
                    "LoadConf",
                    "Cherry",
                    "show run",
                    "25",
                    DateTime.UtcNow.ToString()});
            listViewDetails.Items.Add(item3);
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Task_Edit frm = new Task_Edit();
            DialogResult result = frm.ShowDialog();
            if(result==DialogResult.OK)
            {
                listViewDetails.Items.Clear();
                LoadData();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewDetails.SelectedItems.Count != 0)
            {
                var item = listViewDetails.SelectedItems[0];
                int taskId = Int32.Parse(item.SubItems[0].Text);
                Task_Edit frm = new Task_Edit(taskId);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    listViewDetails.Items.Clear();
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewDetails.SelectedItems.Count != 0)
            {
                var item = listViewDetails.SelectedItems[0];
                int taskId = Int32.Parse(item.SubItems[0].Text);
                //MessageBox.Show(taskId.ToString());
                var queryTask=(from c in context.RemoteTasks
                              where c.Id==taskId
                              select c).FirstOrDefault();

                if (queryTask != null)
                {
                    context.RemoteTasks.Remove(queryTask);
                    context.SaveChanges();
                    listViewDetails.Items.Clear();
                    LoadData();
                }
            }
        }


    }
}
