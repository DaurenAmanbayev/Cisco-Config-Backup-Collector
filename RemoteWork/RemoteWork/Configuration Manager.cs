using RemoteWork.Access;
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
using RemoteWork.Data;

namespace RemoteWork
{
    //перечисление для периода
    enum Period
    {
        LastDay,
        LastDayTenDays,
        LastMonths,
        All,
        BySpecifiedDate
    }
    //перечисление фильтр
    enum FavoriteFilter
    {
        ByIPAddress,
        ByHostname
    }
    public partial class Configuration_Manager : Form
    {
        RconfigContext context;
        List<string> periods;
        string prevHostname;       
        FavoriteFilter filter = FavoriteFilter.ByIPAddress;
        Period period = Period.All;
        //запуск менеджера
        public Configuration_Manager()
        {
            InitializeComponent();
            StartConfiguration();         
            LoadData();
        }
        //запуск менеджера с выбранным устройством
        public Configuration_Manager(string hostname)
        {
            //необходимо выбрать переданное устройство
            InitializeComponent();
            StartConfiguration();            
            prevHostname = hostname;
            filter = FavoriteFilter.ByHostname;
            checkBoxHostname.Checked = true;
            LoadData();
        }
        //начальные настройки приложения
        private void StartConfiguration()
        {
            periods=new List<string>
            {
                "All Days",
                "Last Day",
                "Last 10 Days",
                "Last 30 Days",                
                "By Specified Date"
            };
            comboBoxPeriod.DataSource = periods;
        }
        //подгрузка требуемых данных
        private async void LoadData()
        {
            using (context = new RconfigContext())
            {
                var queryFavs = await (from c in context.Favorites
                                select c).ToListAsync();
                switch (filter)
                {
                    case FavoriteFilter.ByHostname:
                        var queryHostname = from c in queryFavs
                                            select c.Hostname;
                        comboBoxFavs.DataSource = queryHostname.ToList();
                        break;
                    case FavoriteFilter.ByIPAddress:
                        var queryAddresses = from c in queryFavs
                                            select c.Address;
                        comboBoxFavs.DataSource = queryAddresses.ToList();
                        break;
                }
            }          
            if (prevHostname != null)
            {
                comboBoxFavs.SelectedItem = prevHostname;
                LoadConfigurationData(prevHostname);
            }
        }
        //фильтрация по устройству
        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (comboBoxFavs.SelectedItem != null)
            {
                string favorite = comboBoxFavs.SelectedItem.ToString();
                LoadConfigurationData(favorite);
            }
            //Find method
            else
            {
                //необходимо найти устройство по введенной строке и спросить пользователя, требуется ли подгрузить данные
                string favorite=comboBoxFavs.Text.ToString();
                LoadConfigurationData(favorite);
            }
        }
        //не реализовано
        //фильтрация по периоду времени
        private void buttonTime_Click(object sender, EventArgs e)
        {
            //реализация временного фильтра, требуется ли???
        }
        //сбор конфигурационных данных
        private void LoadConfigurationData(string favorite)
        {            
            switch (filter)
            {
                case FavoriteFilter.ByHostname:
                    using (context = new RconfigContext())
                    {
                        var queryByHostname = (from c in context.Favorites
                                               where c.Hostname == favorite
                                               select c).FirstOrDefault();//если использовать Single, выкидывает исключение
                        if (queryByHostname != null)
                        {
                            listViewConfig.Items.Clear();
                            foreach (Config config in queryByHostname.Configs)
                            {
                                var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                listViewConfig.Items.Add(item);
                            }
                        }
                        else
                        {
                            NotifyInfo("Favorite not found!");
                        }
                    }
                    break;
                case FavoriteFilter.ByIPAddress:
                    using (context = new RconfigContext())
                    {
                        var queryByAddress = (from c in context.Favorites
                                              where c.Address == favorite
                                              select c).FirstOrDefault();
                        if (queryByAddress != null)
                        {
                            listViewConfig.Items.Clear();
                            foreach (Config config in queryByAddress.Configs)
                            {
                                var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                listViewConfig.Items.Add(item);
                            }
                        }
                        else
                        {
                            NotifyInfo("Favorite not found!");
                        }
                    }
                    break;
            }
        }
        //проверка изменения фильтра устройства
        private void checkBoxHostname_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxHostname.Checked)
            {
                filter = FavoriteFilter.ByHostname;
                LoadData();
            }
            else
            {
                filter = FavoriteFilter.ByIPAddress;
                LoadData();
            }
        }
        //просмотр конфигурации устройства
        private void openConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfig.SelectedItems.Count != 0)
            {
                var item = listViewConfig.SelectedItems[0];
                int configId = Int32.Parse(item.SubItems[0].Text);
                using (context = new RconfigContext())
                {
                    var queryConfig = (from c in context.Configs
                                       where c.Id == configId
                                       select c).FirstOrDefault();
                    if (queryConfig != null)
                    {
                        Config_Watcher frm = new Config_Watcher(queryConfig);
                        DialogResult result = frm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            //*******************************
                        }
                    }
                }
            }            
        }
        //Уведомления
        private void NotifyInfo(string info)
        {
            MessageBox.Show(info, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
     
    }
}
