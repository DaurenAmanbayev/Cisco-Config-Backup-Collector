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
using RemoteWork.Expect;
using RemoteWork.CommandUsage;
using System.Threading;

namespace RemoteWork.Managers
{
    public partial class Task_Manager : Form
    {
        RconfigContext _context;
        DateTime _start;

        public Task_Manager()
        {
            InitializeComponent();
            LoadData();
            //TaskAddTest();
        }

        //подгрузка данных по задачам
        private async  void LoadData()
        {
            using (_context = new RconfigContext())
            {
                var queryTasks = await (from c in _context.RemoteTasks
                                        select c).ToListAsync();

                if (queryTasks != null)
                {

                    foreach (RemoteTask task in queryTasks)
                    {
                        var item = new ListViewItem(new[] { 
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
        }
        //!!!!удалить тестовый метод
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
        //создание, редактирования и удаление задач
        #region MANAGE
        //создать задачу
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
        //редактирование задачи
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
        //удаление задачи
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewDetails.SelectedItems.Count != 0)
            {
                var item = listViewDetails.SelectedItems[0];
                int taskId = Int32.Parse(item.SubItems[0].Text);
                //MessageBox.Show(taskId.ToString());
                using (_context = new RconfigContext())
                {
                    var queryTask = (from c in _context.RemoteTasks
                                     where c.Id == taskId
                                     select c).FirstOrDefault();

                    if (queryTask != null)
                    {
                        _context.RemoteTasks.Remove(queryTask);
                        _context.SaveChanges();
                        listViewDetails.Items.Clear();
                        LoadData();
                    }
                }
            }
        }
        #endregion

        //запуск задачи
        private void buttonRunTask_Click(object sender, EventArgs e)
        {
            //проверить корректность работы!!
            if (listViewDetails.SelectedItems.Count != 0)
            {
                var item = listViewDetails.SelectedItems[0];
                int taskId = Int32.Parse(item.SubItems[0].Text);
                toolStripStatusLabelRun.Text = "Задача в процессе выполнения. Это может занять несколько минут...";
                //заблокировать родительское поле
                this.MdiParent.Enabled = false;
                _start = DateTime.Now;
                //ПРОБЛЕМА!!!!                
                CommandUsageMode mode = CommandUsageMode.LoopUsage;
                mode = CommandUsageMode.TaskParallelUsage;
                CommandUsage.CommandUsage comm = new CommandUsage.CommandUsage(taskId, mode);               
                //подписываемся на событие о, том что задачи завершены
                comm.taskCompleted += this.UnlockApplicationAfterComplete;
                //вызываем задачу
                comm.Dispatcher();
            }
        }
        //разблокировать родительское поле после завершения задачи
        private void UnlockApplicationAfterComplete()
        {
            TimeSpan diff = DateTime.Now - _start;
            MessageBox.Show(string.Format("Task finished in {0} seconds", diff));
            // разблокировать 
            this.MdiParent.Enabled = true;
            toolStripStatusLabelRun.Text = string.Format("Task finished in {0} seconds", diff.ToString());
        }
    }
}
