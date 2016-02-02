﻿using System;
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


namespace RemoteWork
{   
    public partial class Rconfig : Form
    {
        RconfigContext context=new RconfigContext();
        CancellationToken token;
        List<Task> taskRunnerManager = new List<Task>();
        bool isLock = false;
        public Rconfig()
        {
            InitializeComponent();
            Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<RconfigContext, RemoteWork.Access.Migrations.Configuration>());
            StartConfiguration();
            LoadData();
            LoadTask();
        }

        #region CUSTOM METHODS
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
            LoadChildData();
           
        }
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
        
        //подгружаем список задач
        private async void LoadTask()
        {
            using (context = new RconfigContext())
            {
                var queryTasks = await (from c in context.RemoteTasks
                                        select c.TaskName).ToArrayAsync();

               toolStripComboBoxTasks.Items.AddRange(queryTasks); //почему не выбирает первую модель               
                //добавлять данные в комбо бокс
            }
            //toolStripComboBoxTasks.
        }
        //Уведомления
        private void NotifyInfo(string info)
        {
            MessageBox.Show(info, "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region EXPECT
        private void LoadConfiguration(RemoteTask task)
        {
            //LockForm();
            var tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            ProgressInit(task.Favorites.Count);
            foreach (Favorite fav in task.Favorites)
            {                
                List<string> commands = new List<string>();
                //проходим по списку команд, выявляем соответствие используемой команды и категории избранного               
                foreach (Command command in task.Commands)
                {
                    foreach (Category category in command.Categories)
                    {
                        if (fav.Category.CategoryName == category.CategoryName)
                        {
                            commands.Add(command.Name);
                        }
                    }
                }
                //мультипоточность
                //устанавливаем соединение
                FavoriteConnect connect=new FavoriteConnect();
                connect.commands=commands;
                connect.favorite=fav;
                connect.task=task;
                Task taskRunner = Task.Factory.StartNew(Connection, connect, token);
                /*
                 * t = Task.Factory.StartNew(() => DoSomeWork(1, token), token);
        Console.WriteLine("Task {0} executing", t.Id);
        tasks.Add(t);
                 */
                taskRunnerManager.Add(taskRunner);
                //Connection(fav, commands, task);
            }
            //дожидаемся пока выполняться все задания
            foreach (Task taskRunner in taskRunnerManager)
            {
                ProgressStep();
                taskRunner.Wait();
            }
            //LockForm();
           // NotifyInfo("Success!");
            ProgressClear();
        }
        private void Connection(object favConnect)
        {
            RemoteTask task=((FavoriteConnect)favConnect).task;
            List<string> commands = ((FavoriteConnect)favConnect).commands;
            Favorite fav=((FavoriteConnect)favConnect).favorite;
            //данные для подключения к сетевому устройству
            ConnectionData data = new ConnectionData();
            data.address = fav.Address;
            data.port = fav.Port;
            data.username = fav.Credential.Username;
            data.password = fav.Credential.Password;

            //по типу протоколу выбираем требуемое подключение
            string protocol = fav.Protocol.Name;
            Expect.Expect expect;
            switch (protocol)
            {
                case "Telnet": expect = new TelnetExpect(data); break;
                case "SSH": expect = new SshExpect(data); break;
                //по умолчанию для сетевых устройств протокол Telnet
                default: expect = new TelnetExpect(data); break;
            }
            //если объект expect успешно создан
            if (expect != null)
            {
                //выполняем список команд
                expect.ExecuteCommands(commands);
                string result = expect.GetResult();
                bool success = expect.isSuccess;
                string error = expect.GetError();
                //если успешно сохраняем конфигурацию устройства
                if (success)
                {
                    Config config = new Config();
                    config.Current = result;
                    config.Date = DateTime.UtcNow;
                    fav.Configs.Add(config);
                }
                //создаем отчет о проделанном задании
                Report report = new Report();
                report.Date = DateTime.UtcNow;
                report.Status = success;
                report.Info = error;
                report.Task = task;
                report.Favorite = fav;
                context.Reports.Add(report);
                context.SaveChanges();
            }
        }
        //блокировка формы на время выполнения задачи
        private void LockForm()
        {
            if (isLock)
            {
               
            }
            else
            {
 
            }
            isLock = !isLock;
        }
        #endregion

        #region PROGRESS BAR
        //PROGRESS INIT
        private void ProgressInit(int steps)
        {
            toolStripProgressBarRunner.Maximum = steps;
            toolStripProgressBarRunner.Minimum = 0;
            toolStripProgressBarRunner.Step = 1;
        }
        //PROGRESS CHANGES
        private void ProgressStep()
        {
            toolStripProgressBarRunner.PerformStep();
        }
        //PROGRESS CLEAR
        private void ProgressClear()
        {
            toolStripProgressBarRunner.Value = 0;
        }
        #endregion

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

        private void LoadConfigurationData(string favorite, bool isFavorite)
        {
 
        }
        #endregion

        #region FAVORITE 
        private void favoriteAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                //context.Dispose();
                //context = new RconfigContext();
                treeViewFavorites.Nodes.Clear();
            ///    MessageBox.Show(treeViewFavorites.Nodes.Count.ToString());
              //  treeViewFavorites.Refresh();
                LoadData();
            }
        }
        //!!!! доработать логику обновления данных в treeview
        private void favoriteEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewFavorites.SelectedNode!=null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
              //  string favCategory = treeViewFavorites.SelectedNode.Parent.Text;
              //  int indexFav = treeViewFavorites.SelectedNode.Index;
              //  int indexCategory = treeViewFavorites.SelectedNode.Parent.Index;
               // string index=treeViewFavorites.Nodes.IndexOfKey(favCategory).ToString();
               // MessageBox.Show(favName+favCategory+index);
                Favorite_Edit frm = new Favorite_Edit(favName);
                DialogResult result = frm.ShowDialog();
                if(result==DialogResult.OK)
                {
                    string favorite = frm.GetLastFavorite();
                    LoadFavoriteData(favorite);
                    treeViewFavorites.Nodes.Clear();
                    LoadData();

                    //удалить не требуется проблема с 
                    //Favorite fav = frm.GetLastFavorite();
                    //string editedFavName = fav.Hostname;
                    //string editedCategory = fav.Category.CategoryName;
                    ////если категория была изменена мы удаляем из категории ранее избранное
                    //if (editedCategory != favCategory)
                    //{
                    //    treeViewFavorites.SelectedNode.Remove();
                    //    //foreach(TreeNode node in treeViewFavorites.Nodes.)
                    //}
                    //else
                    //{
                    //    //если категория не была изменена, но изменен избранный
                    //    if (editedFavName != favName)
                    //    {

                    //        TreeNode childNode = new TreeNode();
                    //        childNode.Name = editedFavName;
                    //        childNode.Text = editedFavName;
                    //    }
                    //}
                }
            }
        }

        private void favoriteDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
                //  MessageBox.Show(favName);
                using (context = new RconfigContext())
                {
                    var queryFavorite = (from c in context.Favorites
                                         where c.Hostname == favName
                                         select c).Single();
                    if (queryFavorite != null)
                    {
                        context.Favorites.Remove(queryFavorite);
                        context.SaveChanges();
                        treeViewFavorites.SelectedNode.Remove();
                    }                   
                }
            }
        }
        #endregion

        #region TABCONTROL
        private void tabControlFavInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlFavInfo.SelectedIndex == 1 && treeViewFavorites.SelectedNode != null)
            {
                //подгружаем конфигурационные данные избранного
                if ( treeViewFavorites.SelectedNode.Level == 1)
                {
                    string fav = treeViewFavorites.SelectedNode.Name.ToString();
                    using (context = new RconfigContext())
                    {
                        //  NotifyInfo(fav);
                        var queryFavorite = (from c in context.Favorites
                                             where c.Hostname == fav
                                             select c).FirstOrDefault();
                        if (queryFavorite != null)
                        {
                            listViewConfig.Items.Clear();
                            foreach (Config config in queryFavorite.Configs)
                            {
                                var item = new ListViewItem(new[] { config.Id.ToString(), config.Date.ToString() });
                                listViewConfig.Items.Add(item);
                            }
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

        #endregion

        #region TOOLSTRIP MENU
        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Management frm = new Management();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                //нужно отследить какие окна открывались, чтобы обновить списки
                //или обновлять список нативно каждый раз
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.Analytic frm = new Analytics.Analytic();
            frm.ShowDialog();
        }

        //дочерние данные после добавления!!!
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                //проблемы была с очисткой контекста!!!
              //  context.Dispose();
              //  context = new RconfigContext();
                treeViewFavorites.Nodes.Clear();
            ///    MessageBox.Show(treeViewFavorites.Nodes.Count.ToString());
              //  treeViewFavorites.Refresh();
                LoadData();

                //альтернатива!!!
                // treeViewFavorites.Nodes.Clear();
                //LoadData();
                //LoadChildData();
            }
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;

                Favorite_Edit frm = new Favorite_Edit(favName);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string favorite = frm.GetLastFavorite();
                    LoadFavoriteData(favorite);

                    treeViewFavorites.Nodes.Clear();
                    LoadData();
                }
            }
        }

        private void deleteFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
                //  MessageBox.Show(favName);
                using (context = new RconfigContext())
                {
                    var queryFavorite = (from c in context.Favorites
                                         where c.Hostname == favName
                                         select c).Single();
                    if (queryFavorite != null)
                    {
                        context.Favorites.Remove(queryFavorite);
                        context.SaveChanges();
                        treeViewFavorites.SelectedNode.Remove();
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NotifyInfo("About Info");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyInfo("Help Info!");
        }
        #endregion

        #region TOOLSTRIP BUTTONS
        private void toolStripButtonAddFav_Click(object sender, EventArgs e)
        {
            /**/
            Favorite_Edit frm = new Favorite_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                //проблемы была с очисткой контекста!!!
                //  context.Dispose();
                //  context = new RconfigContext();
                treeViewFavorites.Nodes.Clear();
                ///    MessageBox.Show(treeViewFavorites.Nodes.Count.ToString());
                //  treeViewFavorites.Refresh();
                LoadData();

                //альтернатива!!!
                // treeViewFavorites.Nodes.Clear();
                //LoadData();
                //LoadChildData();
            }
        }

        private void toolStripButtonEditFav_Click(object sender, EventArgs e)
        {
            /**/
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;

                Favorite_Edit frm = new Favorite_Edit(favName);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string favorite = frm.GetLastFavorite();
                    LoadFavoriteData(favorite);

                    treeViewFavorites.Nodes.Clear();
                    LoadData();
                }
            }
        }

        private void toolStripButtonDelFav_Click(object sender, EventArgs e)
        {
            if (treeViewFavorites.SelectedNode != null)
            {
                string favName = treeViewFavorites.SelectedNode.Text;
                //  MessageBox.Show(favName);
                using (context = new RconfigContext())
                {
                    var queryFavorite = (from c in context.Favorites
                                         where c.Hostname == favName
                                         select c).Single();
                    if (queryFavorite != null)
                    {
                        context.Favorites.Remove(queryFavorite);
                        context.SaveChanges();
                        treeViewFavorites.SelectedNode.Remove();
                    }
                }
            }
        }

        private void toolStripButtonReport_Click(object sender, EventArgs e)
        {
            Analytics.Analytic frm = new Analytics.Analytic();
            frm.ShowDialog();
        }
        /*
         TASK RUN CONFIG
         */
        private void toolStripButtonLoadConfig_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxTasks.SelectedItem != null)
            {
                //MessageBox.Show(toolStripComboBoxTasks.SelectedItem.ToString());
                using(context=new RconfigContext())
                {
                    string taskName = toolStripComboBoxTasks.SelectedItem.ToString();
                    var queryTask=(from c in context.RemoteTasks
                                  where c.TaskName==taskName
                                  select c).FirstOrDefault();
                    if (queryTask != null)
                    {
                        LoadConfiguration(queryTask);
                       // UseWaitCursor = true;
                    }
                }
            }

            //выбираем таск 
          //  this.UseWaitCursor = true;
            
        }
        #endregion

        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfig.SelectedItems.Count != 0)
            {
                var item = listViewConfig.SelectedItems[0];
                int configId=Int32.Parse(item.SubItems[0].Text);
                using (context = new RconfigContext())
                {
                    var queryConfig=(from c in context.Configs
                                        where c.Id==configId
                                        select c).FirstOrDefault();
                    if (queryConfig != null)
                    {                        
                        Config_Watcher frm = new Config_Watcher(queryConfig);
                        DialogResult result = frm.ShowDialog();
                        if (result == DialogResult.OK)
                        {

                        }
                    }
                }
            }
        }
       
    }
}
