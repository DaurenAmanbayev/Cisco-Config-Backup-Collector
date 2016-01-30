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
        private async void LoadData()
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
        private void LoadChildData()
        {
            foreach (TreeNode node in treeViewFavorites.Nodes)
            {
                node.Nodes.Clear();
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
            treeViewFavorites.Refresh();
            //не срабатывает при добавлении устройства, при удалении срабатывает
        }

        //дочерние данные после добавления
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // treeViewFavorites.Nodes.Clear();
                //LoadData();
                LoadChildData();
            }
        }

        #region TREEVIEW DATA
        private void treeViewFavorites_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewFavorites.SelectedNode.Level == 0)
            {
                string category = treeViewFavorites.SelectedNode.Name.ToString();
                LoadCategoryData(category);
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
                //вызываем дополнительную информацию по устройству
                string fav = treeViewFavorites.SelectedNode.Name.ToString();
                LoadFavoriteData(fav);
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
        //подгрузка данных при выборе избранного или категории
        private void LoadFavoriteData(string favorite)
        {
            var queryFavorite=(from c in context.Favorites
                              where c.Hostname==favorite
                              select c).FirstOrDefault();
            if (queryFavorite != null)
            {
                listViewDetails.Items.Clear();
                var item = new ListViewItem(new[] { queryFavorite.Hostname,
                    queryFavorite.Address,
                    queryFavorite.Port.ToString(),
                    queryFavorite.Protocol.Name,
                    queryFavorite.Location.LocationName });
                listViewDetails.Items.Add(item);
            }
        }

        private void LoadCategoryData(string category)
        {
            var queryCategory=(from c in context.Categories
                              where c.CategoryName==category
                              select c).FirstOrDefault();

            if (queryCategory != null)
            {
                listViewDetails.Items.Clear();
                foreach (Favorite fav in queryCategory.Favorites)
                {
                    var item = new ListViewItem(new[] { fav.Hostname,
                    fav.Address,
                    fav.Port.ToString(),
                    fav.Protocol.Name,
                    fav.Location.LocationName });
                    listViewDetails.Items.Add(item);
                }
            }
        }
        #endregion

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

        private void tabControlFavInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlFavInfo.SelectedIndex == 1 && treeViewFavorites.SelectedNode != null)
            {
                //подгружаем конфигурационные данные избранного
                if ( treeViewFavorites.SelectedNode.Level == 1)
                {
                    string fav = treeViewFavorites.SelectedNode.Name.ToString();
                    NotifyInfo(fav);
                    var queryFavorite = (from c in context.Favorites
                                         where c.Hostname == fav
                                         select c).FirstOrDefault();
                    if (queryFavorite != null)
                    {
                        listViewConfig.Items.Clear();
                        foreach (Config config in queryFavorite.Configs)
                        {
                            var item = new ListViewItem(new[] { config.Id.ToString(), config.Date.ToString() });
                            listViewDetails.Items.Add(item);
                        }
                    }
                }
                //если была выбрана категория просим выбрать избранное
                else if (treeViewFavorites.SelectedNode.Level == 0)
                {
                    NotifyInfo("Please select favorite to view configuration!");
                }
            }
           
        }
        //Уведомления
        private void NotifyInfo(string info)
        {
            MessageBox.Show(info, "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.Analytic frm = new Analytics.Analytic();
            frm.ShowDialog();
        }
    }
}
