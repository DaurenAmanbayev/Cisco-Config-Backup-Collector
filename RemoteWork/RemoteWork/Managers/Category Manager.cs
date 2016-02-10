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
using System.Data.Entity;

namespace RemoteWork.Managers
{
    public partial class Category_Manager : Form
    {
        RconfigContext context = new RconfigContext();
        public Category_Manager()
        {
            InitializeComponent();
            LoadData();
        }
        //подгражем требуемые данные
        private async void LoadData()
        {
            var queryCategory= await (from c in context.Categories
                               select c.CategoryName).ToListAsync();

            listBoxCategory.DataSource = queryCategory;
        }
        //добавляем новую категорию
        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category_Edit frm = new Category_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }
        //редактируем существующую категорию
        private void editCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxCategory.SelectedItem != null)
            {
                string category = listBoxCategory.SelectedItem.ToString();
                Category_Edit frm = new Category_Edit(category);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        //удаляем существующую категорию
        private void deleteCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxCategory.SelectedItem != null)
            {
                string category = listBoxCategory.SelectedItem.ToString();
                
                var queryCategory=(from c in context.Categories
                                  where c.CategoryName==category
                                  select c).FirstOrDefault();
                if (queryCategory != null)
                {
                    context.Categories.Remove(queryCategory);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }
    }
}
