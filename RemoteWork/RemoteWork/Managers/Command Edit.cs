using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteWork.Access;
using RemoteWork.Data;
using System.Data.Entity;

namespace RemoteWork.Managers
{
    //проверка корректности данных команды
    enum CommandInputValidate
    {
        CommandEmpty,
        CommandNotUnique,
        CommandCategoryNotSelected
    }
    //добавить описание откуда - куда
    //добавить приоритет команды
    //при использовании сортировать по приоритету
    public partial class Command_Edit : Form
    {
        CommandInputValidate validateInput = CommandInputValidate.CommandEmpty;
        RconfigContext context = new RconfigContext();
        WindowsMode mode = WindowsMode.ADD;
        string prevCommand;
        Command currentCommand;       
        public Command_Edit()
        {
            InitializeComponent();
            StartConfiguration();
            LoadData();
        }

        public Command_Edit(string command)
        {
            InitializeComponent();
            mode = WindowsMode.EDIT;
            textBoxName.Text = command;
            prevCommand = command;
            StartConfiguration();
            LoadPrevData();
        }
        //начальные настройки формы
        private void StartConfiguration()
        {
            List<int> order = new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };

            comboBoxOrders.DataSource = order;
        }
        //подгружаем требуемые данные
        private async void LoadData()
        {
            var queryCategories = await (from c in context.Categories
                                         select c.CategoryName).ToListAsync();

            foreach (string category in queryCategories)
            {
                listBoxAvailable.Items.Add(category);
            }          
            
            
        }
        //в случае режима редактирования
        //подгружаем данные выбранной команды
        private async void LoadPrevData()
        {
            var queryCommand = await (from c in context.Commands
                                      where c.Name == prevCommand
                                      select c).FirstOrDefaultAsync();
            if (queryCommand != null)
            {
                //MessageBox.Show(queryCommand.Categories.Count.ToString());
                currentCommand = queryCommand;
                var queryCommandCategories = (from c in currentCommand.Categories
                                              select c.CategoryName).ToList();
                //для избежания проблем с datasource при обновлении списка
                foreach (string category in queryCommandCategories)
                {
                    listBoxCurrent.Items.Add(category);
                }

                var queryCategories = await (from c in context.Categories
                                             select c.CategoryName).ToListAsync();

                foreach (string cat in queryCategories)
                {
                    if (!queryCommandCategories.Contains(cat))
                    {
                        listBoxAvailable.Items.Add(cat);
                    }
                }                
                comboBoxOrders.SelectedItem = currentCommand.Order;
                             
            }
        }
        //проверка на уникальность
        private bool isUnique(string command)
        {
            if (mode == WindowsMode.EDIT && prevCommand == textBoxName.Text.Trim())
            {
                return true;
            }
            bool uniqueCommand = true;
            context.Commands.ToList().ForEach(loc =>
            {
                if (loc.Name == command)
                {
                    uniqueCommand = false;
                    validateInput = CommandInputValidate.CommandNotUnique;
                }

            });

            return uniqueCommand;
        }
        //проверка данных пользователя
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text.Trim()))
            {
                validateInput = CommandInputValidate.CommandEmpty;
                return false;
            }           
            if (listBoxCurrent.Items.Count==0)
            {
                validateInput = CommandInputValidate.CommandCategoryNotSelected;
                return false;
            }
            return isUnique(textBoxName.Text.Trim());
        }
        //добавить команду
        private void AddCommand()
        {
            currentCommand = new Command();
            currentCommand.Name = textBoxName.Text.Trim();
            currentCommand.Order = (int)comboBoxOrders.SelectedItem;
            HashSet<Category> currentCommandCategories = new HashSet<Category>();
            foreach (string category in listBoxCurrent.Items)
            {
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
                                     select c).First();
                if (queryCategory != null)
                    currentCommandCategories.Add(queryCategory);
            }
            currentCommand.Categories = currentCommandCategories;
            context.Commands.Add(currentCommand);
            context.SaveChanges();
        }
        //редактировать команду
        private void EditCommand()
        {
            currentCommand.Name = textBoxName.Text.Trim();
            currentCommand.Order = (int)comboBoxOrders.SelectedItem;
            HashSet<Category> currentCommandCategories = new HashSet<Category>();
            foreach (string category in listBoxCurrent.Items)
            {
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
                                     select c).First();
                if (queryCategory != null)
                    currentCommandCategories.Add(queryCategory);
            }
            currentCommand.Categories = currentCommandCategories;           
            context.Entry(currentCommand).State = System.Data.Entity.EntityState.Modified;            
            context.SaveChanges();
        }
        //подтверждение операции
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                try
                {
                    switch (mode)
                    {
                        case WindowsMode.ADD: AddCommand(); break;
                        case WindowsMode.EDIT: EditCommand(); break;
                    }
                }
                catch (System.Data.Common.DbException ex)
                {
                    NotifyWarning(ex.Message);
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validateInput)
                {
                    case CommandInputValidate.CommandEmpty: NotifyWarning("Command Name is empty!"); break;
                    case CommandInputValidate.CommandNotUnique: NotifyWarning("This command already exist!"); break;
                    case CommandInputValidate.CommandCategoryNotSelected: NotifyWarning("Command category not selected!"); break;
                }
            }
        }
        //отмена операции
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //уведомление
        private void NotifyWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //операции по выбору категорий для команды
        //добавление и удаление
        #region CATEGORY SELECTION
        private void addToCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxAvailable.SelectedItem != null)
            {
                string category = listBoxAvailable.SelectedItem.ToString();

                listBoxCurrent.Items.Add(category);
                listBoxAvailable.Items.Remove(listBoxAvailable.SelectedItem);
            }
        }

        private void addAllToCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            listBoxAvailable.Items.Clear();
            LoadListBoxData(listBoxCurrent);

        }

        private void removeFromCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxCurrent.SelectedItem != null)
            {
                string category = listBoxCurrent.SelectedItem.ToString();
                listBoxAvailable.Items.Add(category);
                listBoxCurrent.Items.Remove(listBoxCurrent.SelectedItem);
            }
        }

        private void removeAllFromCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxCurrent.Items.Clear();
            LoadListBoxData(listBoxAvailable);
        }

        private async void LoadListBoxData(ListBox listBox)
        {
            listBox.Items.Clear();
            var queryCategories = await (from c in context.Categories
                                         select c.CategoryName).ToListAsync();
            foreach (string category in queryCategories)
            {
                listBox.Items.Add(category);
            }
        }
        #endregion
    }
}
