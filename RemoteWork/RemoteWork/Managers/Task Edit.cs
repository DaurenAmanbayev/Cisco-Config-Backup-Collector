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
        List<string> favList = new List<string>();
        WindowsMode mode = WindowsMode.ADD;
        TaskInputValidate validateInput = TaskInputValidate.TaskNameIsEmpty;
        RemoteTask currentTask;
        bool byFavorite = true;        
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
        }
        //подгрузка данных
        private async void LoadData()
        {
            var queryFavs=await (from c in context.Favorites
                          select c.Hostname).ToListAsync();

            foreach (string fav in queryFavs)
            {
                checkedListBoxFavorites.Items.Add(fav);
                //запоминаем список устройств для избежания повторной подгрузки
                favList.Add(fav);
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
                textBoxName.Text = currentTask.TaskName;
                textBoxDesc.Text = currentTask.Description;
                //указываем выбранные ранее устройства
                foreach (Favorite fav in queryRemoteTask.Favorites)
                {
                    int index = checkedListBoxFavorites.Items.IndexOf(fav.Hostname);

                    if (index >= 0)
                    {
                        checkedListBoxFavorites.SetItemChecked(index, true);
                    }
                }
                //указываем ранее выбранные команды
                foreach (Command command in queryRemoteTask.Commands)
                {
                    int index = checkedListBoxCommands.Items.IndexOf(command.Name);
                    if (index >= 0)
                    {
                        checkedListBoxCommands.SetItemChecked(index, true);
                    }
                }
            }
            
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
            //если устройства выбраны по устройствам
            if (byFavorite)
            {
                foreach (var item in checkedListBoxFavorites.CheckedItems)
                {
                    string favorite = item.ToString();
                    var queryFav = (from c in context.Favorites
                                    where c.Hostname == favorite
                                    select c).FirstOrDefault();

                    if (queryFav != null)
                        favs.Add(queryFav);
                }
            }
            //по категориям устройств
            else
            {
                foreach (var item in checkedListBoxFavorites.CheckedItems)
                {
                    string category = item.ToString();
                    var queryCategory = (from c in context.Categories
                                    where c.CategoryName == category
                                    select c).FirstOrDefault();

                    if (queryCategory != null)
                    {
                        foreach (Favorite fav in queryCategory.Favorites)
                        {
                            favs.Add(fav);
                        }
                    }
                }
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
            //если устройства выбраны по устройствам
            if (byFavorite)
            {
                foreach (var item in checkedListBoxFavorites.CheckedItems)
                {
                    string favorite = item.ToString();
                    var queryFav = (from c in context.Favorites
                                    where c.Hostname == favorite
                                    select c).FirstOrDefault();

                    if (queryFav != null)
                        favs.Add(queryFav);
                }
            }
            //по категориям устройств
            else
            {
                foreach (var item in checkedListBoxFavorites.CheckedItems)
                {
                    string category = item.ToString();
                    var queryCategory = (from c in context.Categories
                                         where c.CategoryName == category
                                         select c).FirstOrDefault();

                    if (queryCategory != null)
                    {
                        foreach (Favorite fav in queryCategory.Favorites)
                        {
                            favs.Add(fav);
                        }
                    }
                }
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
        //выбрать все опции
        private void checkBoxCheckAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxCheckAll.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxFavorites.Items.Count; i++)
                {                  
                      checkedListBoxFavorites.SetItemChecked(i, true);                   
                }
               // checkBoxCheckAll.Text = "Uncheck All";
            }
            else
            {
                for (int i = 0; i < checkedListBoxFavorites.Items.Count; i++)
                {
                    checkedListBoxFavorites.SetItemChecked(i, false);
                }
            }
        }
        //выбор опции подгрузки данных
        private void checkBoxByFavorite_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxByCategory.CheckState == CheckState.Checked)
            {
                byFavorite = false;
                labelCategory.Text = "Selected Categories";
                var queryCategories=(from c in context.Categories
                                    select c.CategoryName).ToList();
                if (queryCategories != null)
                {
                    checkedListBoxFavorites.Items.Clear();
                    foreach (string category in queryCategories)
                    {
                        checkedListBoxFavorites.Items.Add(category);
                    }
                }
            }
            else
            {
                byFavorite = true;
                labelCategory.Text = "Selected Favorites";
                checkedListBoxFavorites.Items.Clear();
                foreach (string fav in favList)
                {
                    checkedListBoxFavorites.Items.Add(fav);
                }
                LoadPrevData();
            }
        }

       
    }
}
