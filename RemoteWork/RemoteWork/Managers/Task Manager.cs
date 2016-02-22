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
        RconfigContext context;
        DateTime start;
        public Task_Manager()
        {
            InitializeComponent();
            LoadData();
            //TaskAddTest();
        }
        //подгрузка данных по задачам
        private async  void LoadData()
        {
            using (context = new RconfigContext())
            {
                var queryTasks = await (from c in context.RemoteTasks
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
                using (context = new RconfigContext())
                {
                    var queryTask = (from c in context.RemoteTasks
                                     where c.Id == taskId
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
        #endregion
        //запуск задачи
        private void buttonRunTask_Click(object sender, EventArgs e)
        {
            if (listViewDetails.SelectedItems.Count != 0)
            {
                var item = listViewDetails.SelectedItems[0];
                int taskId = Int32.Parse(item.SubItems[0].Text);
                toolStripStatusLabelRun.Text = "Task in progress and still doing some times... Please Wait!";
                //this.Parent.UseWaitCursor = true;          
                //this.Enabled = false;
                this.MdiParent.Enabled = false;
                start = DateTime.Now;
                //ПРОБЛЕМА!!!!                
                CommandUsageMode mode = CommandUsageMode.LoopUsage;
                switch (checkBoxThreading.CheckState)
                {
                    case CheckState.Checked: mode = CommandUsageMode.TaskParallelUsage; break;
                    case CheckState.Unchecked: mode = CommandUsageMode.LoopUsage; break;
                }
                CommandUsage.CommandUsage comm = new CommandUsage.CommandUsage(taskId, mode);               
                //подписываемся на событие о, том что задачи завершены
                comm.taskCompleted += this.UnlockApplicationAfterComplete;
                //вызываем задачу
                comm.Dispatcher();

                #region CHILD TASK PROGRESS
                //MessageBox.Show("Операция может занять несколько минут!");
                //UseWaitCursor = true;               
                //DateTime start = DateTime.Now;
                ////ПРОБЛЕМА!!!!
                //CommandUsageMode mode = CommandUsageMode.LoopUsage;
                //CommandUsage.CommandUsage comm = new CommandUsage.CommandUsage(taskId, mode);
                //comm.Dispatcher();
                //TimeSpan diff = DateTime.Now - start;
                //UseWaitCursor = false;
                //MessageBox.Show("Время затраченная на операцию {0}", diff.ToString());
                //Task_Progress frm = new Task_Progress(taskId);
                //DialogResult result = frm.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    frm.Close();//прогресс бар
                //}    
                #endregion
            }
        }
        //разблокировать
        private void UnlockApplicationAfterComplete()
        {
            TimeSpan diff = DateTime.Now - start;
            // Thread.Sleep(2500);             
            //this.Enabled = true;
            this.MdiParent.Enabled = true;
            toolStripStatusLabelRun.Text = string.Format("Task finished in {0} seconds", diff.ToString());
        }
    }
}
