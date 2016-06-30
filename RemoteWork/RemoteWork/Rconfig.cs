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
добавить фильтр в репорт
добавить функционал поиска по значению
добавить ордер для команды
добавить выбор категорий в задачи
*/
namespace RemoteWork
{   
    public partial class Rconfig : Form
    {
        RconfigContext context=new RconfigContext();   
        //реализовать проверку, что база данных уже была создана
        //переписать кастомный метод логгирования на стандартный метод
        //добавить гибкий поиск по устройствам, с помощью гибких параметризированных запросов
        public Rconfig()
        {
            InitializeComponent();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RconfigContext, Access.Migrations.Configuration>());
            StartConfiguration();
            FirstInsert();
            LoadData();         
        }

        //дополнительные методы используемые в форме
        #region CUSTOM METHODS
        //стартовые настройки интерфейса
        private void StartConfiguration()
        {
            //контекстное меню категории           
            addFavoriteToolStripMenuItem.Visible = false;      
         
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
        //вставка данных по умолчанию
        private void FirstInsert()
        {
            using (RconfigContext context = new RconfigContext())
            {
                //проверяем данные в БД
                //если данных нет инициализируем их
                if (context.Protocols.Count<Protocol>() == 0)
                {
                    //PROTOCOLS
                    context.Protocols.Add(new Protocol
                    {
                        Name = "SSH",
                        DefaultPort = 22
                    });
                    context.Protocols.Add(new Protocol
                    {
                        Name = "Telnet",
                        DefaultPort = 23
                    });

                    //CATEGORIES
                    Category routers = new Category
                    {
                        CategoryName = "Routers"
                    };

                    Category switches = new Category
                    {
                        CategoryName = "Switches"
                    };

                    Category servers = new Category
                    {
                        CategoryName = "Servers"
                    };

                    context.Categories.Add(routers);
                    context.Categories.Add(switches);
                    context.Categories.Add(servers);

                    //COMMANDS
                    ICollection<Category> cisco = new HashSet<Category>();
                    cisco.Add(routers);
                    cisco.Add(switches);

                    context.Commands.Add(new Command
                    {
                        Name = "terminal length 0",
                        Order = 0,
                        Categories = cisco
                    });

                    context.Commands.Add(new Command
                    {
                        Name = "show running-config",
                        Order = 1,
                        Categories = cisco
                    });
                    ICollection<Category> vlan = new HashSet<Category>();
                    vlan.Add(switches);
                    context.Commands.Add(new Command
                    {
                        Name = "show ip vlan brief",
                        Order = 2,
                        Categories = vlan
                    });

                    //CREDENTIALS
                    context.Credentials.Add(new Credential
                    {
                        CredentialName = "Default",
                        Username = "root",
                        Domain = "domain.com",
                        Password = "password"
                    });
                    //LOCATIONS
                    context.Locations.Add(new Location
                    {
                        LocationName = "Syslocation"
                    });
                    context.SaveChanges();
                }
            }
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
                addFavoriteToolStripMenuItem.Visible = true;               
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
        private void addFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
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
        //добавление нового устройства
        private void toolStripButtonAddFavorite_Click(object sender, EventArgs e)
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
        private void appManagementToolStripMenuItem_Click(object sender, EventArgs e)
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
            RconfigImporter frm = new RconfigImporter();
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
            NotifyInfo("Project on GitHub https://github.com/DaurenAmanbayev/RemoteWork");
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
            RconfigImporter frm = new RconfigImporter();
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
            NotifyInfo("Project on GitHub https://github.com/DaurenAmanbayev/RemoteWork ");
        }
        private void toolStripButtonInfo_Click(object sender, EventArgs e)
        {
            NotifyInfo("Help Info!");
        }
        #endregion

        //поиск по указанному паттерну
        #region SEARCH
        //поиск избранного устройства
        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(toolStripComboBoxSearch.Text))
            {
                UseWaitCursor = true;
                //MessageBox.Show(toolStripComboBoxSearch.Text);
                using (RconfigContext context = new RconfigContext())
                {
                    bool isCleared = false;//для проверка очищался у нас listview
                    string pattern = toolStripComboBoxSearch.Text.Trim();
                    var queryFavorites = from c in context.Favorites
                                         select c;
                    //nested query
                    var filterQuery = queryFavorites;

                    if (byAddressToolStripMenuItem.CheckState == CheckState.Checked)//фильтр поиска
                    {
                        //ищем схожести по паттерну
                        filterQuery = from c in queryFavorites
                                      where c.Address.Contains(pattern) && c.Address.EndsWith(pattern)
                                      select c;
                        //если запрос не пустой
                        if (filterQuery != null)
                        {
                            //делаем проверку очищался у нас listview
                            if (!isCleared)
                            {
                                //если не очищался, очищаем
                                listViewDetails.Items.Clear();
                                isCleared = true;
                            }
                            //добавляем наши устройства
                            AddFavoritesFromQuery(filterQuery);
                        }
                    }
                    if (byHostnameToolStripMenuItem.CheckState == CheckState.Checked)//фильтр поиска
                    {
                        filterQuery = from c in queryFavorites
                                      where c.Hostname.Contains(pattern) && c.Hostname.EndsWith(pattern)
                                      select c;
                        if (filterQuery != null)
                        {
                            if (!isCleared)
                            {
                                listViewDetails.Items.Clear();
                                isCleared = true;
                            }
                            AddFavoritesFromQuery(filterQuery);
                        }
                    }
                    if (byLocationToolStripMenuItem.CheckState == CheckState.Checked)//фильтр поиска
                    {
                        filterQuery = from c in queryFavorites
                                      where c.Location.LocationName.Contains(pattern) && c.Location.LocationName.EndsWith(pattern)
                                      select c;
                        if (filterQuery != null)
                        {
                            if (!isCleared)
                            {
                                listViewDetails.Items.Clear();
                                isCleared = true;
                            }
                            AddFavoritesFromQuery(filterQuery);
                        }
                    }
                }
                UseWaitCursor = false;
            }
        }
        //добавить устройства из запроса
        private void AddFavoritesFromQuery(IQueryable<Favorite> queryFavorites)
        {
            foreach (Favorite fav in queryFavorites)
            {
                var item = new ListViewItem(new[] { fav.Hostname,
                                    fav.Address,
                                    fav.Port.ToString(),
                                    fav.Protocol.Name,
                                    fav.Location.LocationName });
                listViewDetails.Items.Add(item);
            }
        }     
        #endregion
    }
}
