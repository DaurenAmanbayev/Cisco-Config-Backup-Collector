using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using RemoteWork.Access;
using System.Data.Entity;
using RemoteWork.Data;
using RemoteWork.Expect;

/*
добавление устройств по аналогии с KeePass
 * добавить фильтр в репорт
добавить функционал поиска по значению
добавить ордер для команды
добавить выбор категорий в задачи
*/
namespace RemoteWork
{   
    public partial class Rconfig : Form
    {
        RconfigContext context=new RconfigContext();     
        public Rconfig()
        {
            InitializeComponent();
            Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<RconfigContext, RemoteWork.Access.Migrations.Configuration>());
            StartConfiguration();
            LoadData();         
        }
        //дополнительные методы используемые в форме
        #region CUSTOM METHODS
        //стартовые настройки интерфейса
        private void StartConfiguration()
        {
            //контекстное меню категории
            addCategoryToolStripMenuItem.Visible = false;
            editCategoryToolStripMenuItem.Visible = false;
            deleteCategoryToolStripMenuItem.Visible = false; 
         
        }
        //подгрузка данных о категории для отображения
        private async void LoadData()
        {
            using (context = new RconfigContext())
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
            }
            //подгружать будем только данные верхнего уровня каталоги
            //LoadChildData();
           
        }

        //!!! не используется
        //подгружаем дочерние данные избранные
        private void LoadChildData()
        {
            using (context = new RconfigContext())
            {
                foreach (TreeNode node in treeViewFavorites.Nodes)
                {
                    // node.Nodes.Clear();//при использовании альтернативы
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
            //treeViewFavorites.Refresh();
            //не срабатывает при добавлении устройства, при удалении срабатывает
        }               
        //Уведомления
        private void NotifyInfo(string info)
        {
            MessageBox.Show(info, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion       

        //управление данными в дереве
        #region TREEVIEW DATA
        //для подгрузки данных об устройствах при выборе категории
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
                //favoriteAddToolStripMenuItem.Visible = false;
                //favoriteEditToolStripMenuItem.Visible = false;
                //favoriteDeleteToolStripMenuItem.Visible = false;
            }
            //не требуется из-за того, что данные о сетевых устройствах будут подтягиваться в сам интерфейс
            //else if (treeViewFavorites.SelectedNode.Level == 1)
            //{
            //    //вызываем дополнительную информацию по устройству
            //    string fav = treeViewFavorites.SelectedNode.Name.ToString();
            //    LoadFavoriteData(fav);
            //    //контекстное меню категории
            //    addCategoryToolStripMenuItem.Visible = false;
            //    editCategoryToolStripMenuItem.Visible = false;
            //    deleteCategoryToolStripMenuItem.Visible = false;
            //    //контекстное меню избранного
            //    favoriteAddToolStripMenuItem.Visible = true;
            //    favoriteEditToolStripMenuItem.Visible = true;
            //    favoriteDeleteToolStripMenuItem.Visible = true;
            //}
        }
        //подгрузка данных при выборе избранного
        //!!! не используется
        private void LoadFavoriteData(string favorite)
        {
            using (context = new RconfigContext())
            {
                var queryFavorite = (from c in context.Favorites
                                     where c.Hostname == favorite
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
        }
        //подгрузка данных при выборе категории
        private void LoadCategoryData(string category)
        {
            using (context = new RconfigContext())
            {
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
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
        }
        #endregion

        //добавление, редактирование и удаление устройства, а также просмотр конфигурации по устройству
        #region FAVORITE 
        //запуск единовременного сбор конфигурации
        private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null && listViewDetails.SelectedItems.Count != 0)
            {
                string category = treeViewFavorites.SelectedNode.Text;
                var item = listViewDetails.SelectedItems[0];
                string favName = item.SubItems[0].Text;
                Load_Config frm = new Load_Config(favName, category);
                frm.ShowDialog();
            }
        }
        //просмотр конфигурации
        private void seeConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null && listViewDetails.SelectedItems.Count != 0)
            {
                //string category = treeViewFavorites.SelectedNode.Text;
                var item = listViewDetails.SelectedItems[0];
                string favName = item.SubItems[0].Text;
                Configuration_Manager frm = new Configuration_Manager(favName);
                frm.ShowDialog();
            }
        }
       
        //добавление нового устройства
        private void favoriteAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string category = treeViewFavorites.SelectedNode.Text;
                Favorite_Edit frm = new Favorite_Edit();
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadCategoryData(category);
                }
            }
        }
        //редактирование устройства
        private void favoriteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null && listViewDetails.SelectedItems.Count != 0)
            {
                string category = treeViewFavorites.SelectedNode.Text;
                var item = listViewDetails.SelectedItems[0];
                string favName = item.SubItems[0].Text;

                Favorite_Edit frm = new Favorite_Edit(favName);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadCategoryData(category);                  
                }
            }
            else
            {
                NotifyInfo("Please select favorite to edit!");
            }
        }
        //удаление устройства
        private void favoriteDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null && listViewDetails.SelectedItems.Count != 0)
            {
                string category = treeViewFavorites.SelectedNode.Text;
                var item = listViewDetails.SelectedItems[0];
                string favName = item.SubItems[0].Text;
                bool isChanged = false;
                using (context = new RconfigContext())
                {
                    var queryFavorite = (from c in context.Favorites
                                         where c.Hostname == favName
                                         select c).Single();
                    if (queryFavorite != null)
                    {
                        queryFavorite.Configs.Clear();//требуется очищать дочерние таблицы данных
                        context.Favorites.Remove(queryFavorite);
                        context.SaveChanges();
                        isChanged = true;
                    }
                }
                //для избежания конфликта контекстов
                if (isChanged)
                    LoadCategoryData(category);
            }
            else
            {
                NotifyInfo("Please select favorite to edit!");
            }
        }        
        #endregion       
     
        //опции главного меню
        #region TOOLSTRIP MENU
        //управление данными приложения
        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Management frm = new Management();
            frm.ShowDialog();
            //отслеживаем изменялись ли категории и обновляем данные, если да
            if (frm.CategoryChanged)
            {               
                treeViewFavorites.Nodes.Clear();
                LoadData();               
            }
        }
        //просмотр и управление конфигурациями
        private void configurationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration_Manager frm = new Configuration_Manager();
            frm.ShowDialog();
        }
        //настройка конфигурации приложения
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RconfigSettings frm = new RconfigSettings();
            frm.ShowDialog();    
        }      
        //настройка отчетности по выполнению задач по сбору конфигурации
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.Analytic frm = new Analytics.Analytic();
            frm.ShowDialog();
        }       
        //выход из программы       
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
        //о программе
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NotifyInfo("About Info");
        }
        //справочная информация
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyInfo("Help Info!");
        }
        #endregion

        //кнопки быстрого доступа
        #region TOOLSTRIP BUTTONS
        //настройки приложения
        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            RconfigSettings frm = new RconfigSettings();
            frm.ShowDialog();           
        }
        //отчетность о ходе выполнении задач
        private void toolStripButtonReport_Click_1(object sender, EventArgs e)
        {
            Analytics.Analytic frm = new Analytics.Analytic();
            frm.ShowDialog();
        }
        //просмотр конфигураций
        private void toolStripButtonConfigs_Click(object sender, EventArgs e)
        {
            Configuration_Manager frm = new Configuration_Manager();
            frm.ShowDialog();
        }
        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            NotifyInfo("About Info");
        }
        #endregion    

       
       
    }
}
