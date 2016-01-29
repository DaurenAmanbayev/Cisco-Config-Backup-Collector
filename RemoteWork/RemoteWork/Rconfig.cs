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
using RemoteWork.Data;

namespace RemoteWork
{
    public partial class Rconfig : Form
    {
        RconfigContext context = new RconfigContext();
        public Rconfig()
        {
            InitializeComponent();
            Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<RconfigContext, RemoteWork.Access.Migrations.Configuration>());
            StartConfiguration();
            LoadData();
        }

        private void StartConfiguration()
        {
            //контекстное меню категории
           // addCategoryToolStripMenuItem.Visible = false;
            editCategoryToolStripMenuItem.Visible = false;
            deleteCategoryToolStripMenuItem.Visible = false;
            //контекстное меню избранного
           // favoriteAddToolStripMenuItem.Visible = false;
            favoriteEditToolStripMenuItem.Visible = false;
            favoriteDeleteToolStripMenuItem.Visible = false;
        }
        //подгрузка данных для отображения
        public async void LoadData()
        {
            var queryCategory = await (from c in context.Categories
                                       select c).ToListAsync();
            foreach (Category category in queryCategory)
            {
                TreeNode node = new TreeNode();
                node.Name = category.CategoryName;
                node.Text = category.CategoryName;
                //foreach (Favorite favorite in category.Favorites)
                //{
                //    TreeNode childNode = new TreeNode();
                //    childNode.Text = favorite.Hostname;
                //}
                treeViewFavorites.Nodes.Add(node);
            }
            LoadChildData();
        }
        //подгружаем дочерние данные избранные
        public void LoadChildData()
        {
            foreach (TreeNode node in treeViewFavorites.Nodes)
            {
                string nodeGroup = node.Text;
                var queryGroup = (from category in context.Categories
                                  where category.CategoryName == nodeGroup
                                  select category).FirstOrDefault();
                if (queryGroup != null)
                {
                    foreach (Favorite fav in queryGroup.Favorites)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Name = fav.Hostname;
                        childNode.Text = fav.Hostname;
                        node.Nodes.Add(childNode);
                    }
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
               // treeViewFavorites.Nodes.Clear();
               // LoadData();
            }
        }

        private void treeViewFavorites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewFavorites.SelectedNode.Level == 0)
            {
                //контекстное меню категории
                addCategoryToolStripMenuItem.Visible = true;             
                editCategoryToolStripMenuItem.Visible = true;
                deleteCategoryToolStripMenuItem.Visible = true;
                //контекстное меню избранного
                favoriteAddToolStripMenuItem.Visible = false;
                favoriteEditToolStripMenuItem.Visible = false;
                favoriteDeleteToolStripMenuItem.Visible = false;
            }
            else if (treeViewFavorites.SelectedNode.Level == 1)
            {
                //контекстное меню категории
                addCategoryToolStripMenuItem.Visible = false;
                editCategoryToolStripMenuItem.Visible = false;
                deleteCategoryToolStripMenuItem.Visible = false;
                //контекстное меню избранного
                favoriteAddToolStripMenuItem.Visible = true;
                favoriteEditToolStripMenuItem.Visible = true;
                favoriteDeleteToolStripMenuItem.Visible = true;
            }
        }

        #region FAVORITE 
        private void favoriteAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // treeViewFavorites.Nodes.Clear();
                // LoadData();
            }
        }
        //!!!! доработать логику обновления данных в treeview
        private void favoriteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewFavorites.SelectedNode!=null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
                string favCategory = treeViewFavorites.SelectedNode.Parent.Text;
                int indexFav = treeViewFavorites.SelectedNode.Index;
                int indexCategory = treeViewFavorites.SelectedNode.Parent.Index;
               // string index=treeViewFavorites.Nodes.IndexOfKey(favCategory).ToString();
               // MessageBox.Show(favName+favCategory+index);
                Favorite_Edit frm = new Favorite_Edit(favName);
                DialogResult result = frm.ShowDialog();
                if(result==DialogResult.OK)
                {
                    Favorite fav = frm.GetLastFavorite();
                    string editedFavName = fav.Hostname;
                    string editedCategory = fav.Category.CategoryName;
                    //если категория была изменена мы удаляем из категории ранее избранное
                    if (editedCategory != favCategory)
                    {
                        treeViewFavorites.SelectedNode.Remove();
                        //foreach(TreeNode node in treeViewFavorites.Nodes.)
                    }
                    else
                    {
                        //если категория не была изменена, но изменен избранный
                        if (editedFavName != favName)
                        {

                            TreeNode childNode = new TreeNode();
                            childNode.Name = editedFavName;
                            childNode.Text = editedFavName;
                        }
                    }
                }
            }
        }

        private void favoriteDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
                //  MessageBox.Show(favName);
                var queryFavorite=(from c in context.Favorites
                                  where c.Hostname==favName
                                  select c).Single();
                if (queryFavorite != null)
                {
                    context.Favorites.Remove(queryFavorite);
                    context.SaveChanges();
                }
                treeViewFavorites.SelectedNode.Remove();
            }
        }
        #endregion

        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Management frm = new Management();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
 
            }
        }
     

    }
}
