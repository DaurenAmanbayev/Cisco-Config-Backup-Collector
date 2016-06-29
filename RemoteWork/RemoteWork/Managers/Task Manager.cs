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
        private RconfigContext _context;
        private DateTime _start;
        private int _taskId;
        public static ManualResetEventSlim waitHandle = new ManualResetEventSlim(false);
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
                _taskId = Int32.Parse(item.SubItems[0].Text);
                // this.MdiParent.Enabled = false;
                _start = DateTime.Now;
                object result = WaitWindow.Show(CollectorMethod);
                //укажем прогресс
                TimeSpan diff = DateTime.Now - _start;
                MessageBox.Show(string.Format("Task finished in {0} seconds", diff));
                // разблокировать 
                // this.MdiParent.Enabled = true;
                toolStripStatusLabelRun.Text = string.Format("Task finished in {0} seconds", diff.ToString());
            }
        }

        private void CollectorMethod(object sender, WaitWindowEventArgs e)
        {
            //заблокировать родительское поле
           
            //ПРОБЛЕМА!!!!                
            CommandUsageMode mode = CommandUsageMode.TaskParallelUsage;
            //mode = CommandUsageMode.TaskParallelUsage;
            CommandUsage.CommandUsage comm = new CommandUsage.CommandUsage(_taskId, mode);
            //подписываемся на событие о, том что задачи завершены
            comm.taskCompleted += this.UnlockApplicationAfterComplete;
            //вызываем задачу
            comm.Dispatcher();
            //запускаем ожидание пока не выполняться все задачи
            waitHandle.Wait();
        }

        //разблокировать родительское поле после завершения задачи
        private void UnlockApplicationAfterComplete()
        {
            //разблокируем наше событие
            waitHandle.Set();
        }
    }
}
