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
    //добавить адаптивности в интерфейс!!!!
    //проверка корректности данных задачи
    enum TaskInputValidate
    {
        TaskNameIsEmpty,
        TaskFavsNotChecked,
        TaskCommandsNotChecked
    }
    public partial class Task_Edit : Form
    {
        RconfigContext context = new RconfigContext();
        WindowsMode mode = WindowsMode.ADD;
        TaskInputValidate validateInput = TaskInputValidate.TaskNameIsEmpty;
        RemoteTask currentTask;
        int prevTaskId;
        public Task_Edit()
        {
            InitializeComponent();
            LoadData();
        }
        public Task_Edit(int taskId)
        {
            InitializeComponent();
            mode = WindowsMode.EDIT;
            prevTaskId = taskId;
            LoadData();
            //LoadPrevData();//реализовать!!!
        }
        //подгрузка данных
        private async void LoadData()
        {
            var queryFavs=await (from c in context.Favorites
                          select c).ToListAsync();

            foreach (Favorite fav in queryFavs)
            {
                checkedListBoxFavorites.Items.Add(fav.Hostname);
            }

            var queryCommands = await (from c in context.Commands
                                      select c.Name).ToListAsync();

            foreach (string command in queryCommands)
            {
                checkedListBoxCommands.Items.Add(command);
            }
            if (mode == WindowsMode.EDIT)
                LoadPrevData();
        }
        //подгрузка данных при редактировании
        //не работает!!!
        private async void LoadPrevData()
        {            
            var queryRemoteTask=await (from c in context.RemoteTasks
                                where c.Id==prevTaskId
                                select c).FirstOrDefaultAsync();
            if (queryRemoteTask != null)
            {
                currentTask = queryRemoteTask;
            }
            textBoxName.Text = currentTask.TaskName;
            textBoxDesc.Text = currentTask.Description;
        }
        //проверка данных пользователя
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                return false;
            }
            if (checkedListBoxFavorites.CheckedItems.Count == 0)
            {
                validateInput = TaskInputValidate.TaskFavsNotChecked;
                return false;
            }
            if (checkedListBoxCommands.CheckedItems.Count == 0)
            {
                validateInput = TaskInputValidate.TaskCommandsNotChecked;
                return false;
            }
            return true;
        }
        //добавить задачу
        private void TaskAdd()
        {
            currentTask = new RemoteTask();
            currentTask.TaskName = textBoxName.Text.Trim();
            currentTask.Description=textBoxDesc.Text.Trim();
            currentTask.Date = DateTime.UtcNow;

            HashSet<Favorite> favs = new HashSet<Favorite>();
            foreach (var item in checkedListBoxFavorites.CheckedItems)
            {
                string favorite = item.ToString();
                var queryFav=(from c in context.Favorites
                             where c.Hostname==favorite
                             select c).FirstOrDefault();

                if (queryFav != null)
                    favs.Add(queryFav);
            }
            currentTask.Favorites = favs;
            HashSet<Command> commands = new HashSet<Command>();
            foreach (var item in checkedListBoxCommands.CheckedItems)
            {
                string command = item.ToString();
                var queryCommand = (from c in context.Commands
                                where c.Name== command
                                select c).FirstOrDefault();

                if (queryCommand != null)
                    commands.Add(queryCommand);
            }
            currentTask.Commands = commands;
            context.RemoteTasks.Add(currentTask);
            context.SaveChanges();
        }
        //изменить задачу
        private void TaskEdit()
        {           
            currentTask.TaskName = textBoxName.Text.Trim();
            currentTask.Description = textBoxDesc.Text.Trim();
            currentTask.Date = DateTime.UtcNow;

            HashSet<Favorite> favs = new HashSet<Favorite>();
            foreach (var item in checkedListBoxFavorites.CheckedItems)
            {
                string favorite = item.ToString();
                var queryFav = (from c in context.Favorites
                                where c.Hostname == favorite
                                select c).FirstOrDefault();

                if (queryFav != null)
                    favs.Add(queryFav);
            }
            currentTask.Favorites = favs;
            HashSet<Command> commands = new HashSet<Command>();
            foreach (var item in checkedListBoxCommands.CheckedItems)
            {
                string command = item.ToString();
                var queryCommand = (from c in context.Commands
                                    where c.Name == command
                                    select c).FirstOrDefault();

                if (queryCommand != null)
                    commands.Add(queryCommand);
            }
            currentTask.Commands = commands;
            context.Entry(currentTask).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        //подтверждение операции
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                switch (mode)
                {
                    case WindowsMode.ADD: TaskAdd(); break;
                    case WindowsMode.EDIT: TaskEdit(); break;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validateInput)
                {
                    case TaskInputValidate.TaskNameIsEmpty: NotifyWarning("Task Name input is empty!"); break;
                    case TaskInputValidate.TaskFavsNotChecked: NotifyWarning("Favorites for task not checked!"); break;
                    case TaskInputValidate.TaskCommandsNotChecked: NotifyWarning("Commands for task not checked!"); break;
                }
            }
        }
        //отмена операции
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //уведомление
        private void NotifyWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }   
    }
}
