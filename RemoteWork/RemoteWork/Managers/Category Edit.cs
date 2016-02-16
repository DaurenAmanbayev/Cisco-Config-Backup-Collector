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
    //проверка данных категории
    enum CategoryInputValidate
    {
        CategoryEmpty,
        CategoryNotUnique
    }
    public partial class Category_Edit : Form
    {
        CategoryInputValidate validateInput = CategoryInputValidate.CategoryEmpty;
        RconfigContext context = new RconfigContext();
        Category currentCategory;
        string prevCategory;
        WindowsMode mode = WindowsMode.ADD;
        public Category_Edit()
        {
            InitializeComponent();
        }
        public Category_Edit(string category)
        {
            InitializeComponent();
            mode = WindowsMode.EDIT;
            textBoxCategory.Text = category;
            prevCategory = category;
            LoadData();
        }
        //подгрузка требуемых данных
        private async void LoadData()
        {
            var queryCategory = await (from c in context.Categories
                                       where c.CategoryName == prevCategory
                                       select c).FirstOrDefaultAsync();
            currentCategory = queryCategory;
            //проверка на режим доступа
            if (currentCategory.EnableModeRequired)
                checkBoxEnableMode.CheckState = CheckState.Checked;
            else
                checkBoxEnableMode.CheckState = CheckState.Unchecked;

        }
        //проверка на уникальность устройства
        private bool isUnique(string category)
        {
            if (mode == WindowsMode.EDIT && prevCategory == textBoxCategory.Text.Trim())
            {
                return true;
            }
            bool uniqueCommand = true;
            context.Categories.ToList().ForEach(loc =>
            {
                if (loc.CategoryName == category)
                {
                    uniqueCommand = false;
                    validateInput = CategoryInputValidate.CategoryNotUnique;
                }

            });

            return uniqueCommand;
        }
        //проверка введеных данных пользователя
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxCategory.Text.Trim()))
            {
                return false;
            }           
            return isUnique(textBoxCategory.Text.Trim());
        }
        //подтверждение операции
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                switch (mode)
                {
                    case WindowsMode.ADD:
                        currentCategory = new Category();
                        currentCategory.CategoryName = textBoxCategory.Text.Trim();
                        if (checkBoxEnableMode.CheckState == CheckState.Checked)
                        {
                            currentCategory.EnableModeRequired = true;
                        }
                        else
                        {
                            currentCategory.EnableModeRequired = false;
                        }
                        context.Categories.Add(currentCategory);
                        context.SaveChanges();
                        break;
                    case WindowsMode.EDIT:
                        currentCategory.CategoryName = textBoxCategory.Text.Trim();
                        if (checkBoxEnableMode.CheckState == CheckState.Checked)
                        {
                            currentCategory.EnableModeRequired = true;
                        }
                        else
                        {
                            currentCategory.EnableModeRequired = false;
                        }
                        context.Entry(currentCategory).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        break;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validateInput)
                {
                    case CategoryInputValidate.CategoryEmpty: NotifyWarning("Category Name is empty!"); break;
                    case CategoryInputValidate.CategoryNotUnique: NotifyWarning("Category Name is already exist!"); break;
                }
            }
        }
        //отмена операции
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        //уведомление пользователя
        private void NotifyWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
