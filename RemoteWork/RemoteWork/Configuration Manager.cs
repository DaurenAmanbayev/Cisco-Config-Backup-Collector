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
using RemoteWork.Analytics;

namespace RemoteWork
{    
    //перечисление фильтр
    enum FavoriteFilter
    {
        ByIPAddress,
        ByHostname
    }
    public partial class Configuration_Manager : Form
    {
        RconfigContext context;        
        string prevHostname;       
        FavoriteFilter filter = FavoriteFilter.ByIPAddress;
        DateFilter dateFilter = new DateFilter();
        bool useDateFilter = false;
        //запуск менеджера
        public Configuration_Manager()
        {
            InitializeComponent();       
            LoadData();
        }
        //запуск менеджера с выбранным устройством
        public Configuration_Manager(string hostname)
        {
            //необходимо выбрать переданное устройство
            InitializeComponent();                     
            prevHostname = hostname;
            filter = FavoriteFilter.ByHostname;
            checkBoxHostname.Checked = true;
            LoadData();
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
                            //пройтись по списку конфигураций устройства, отсортированный по ID
                            foreach (Config config in queryByHostname.Configs.OrderByDescending(p => p.Id)) 
                            {
                                //если используется фильтр по дате
                                if (useDateFilter)
                                {
                                    if (config.Date.Value.Year == dateFilter.Year
                                        & config.Date.Value.Month == dateFilter.Month
                                        & config.Date.Value.Day == dateFilter.Day)
                                    {
                                        var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                        listViewConfig.Items.Add(item);
                                    }
                                }
                                //если не используется фильтр по времени
                                else
                                {
                                    var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                    listViewConfig.Items.Add(item);
                                }
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
                            //пройтись по списку конфигураций устройства, отсортированный по ID
                            foreach (Config config in queryByAddress.Configs.OrderByDescending(p => p.Id))
                            {
                                //если используется фильтр по дате
                                if (useDateFilter)
                                {
                                    if (config.Date.Value.Year == dateFilter.Year
                                        & config.Date.Value.Month == dateFilter.Month
                                        & config.Date.Value.Day == dateFilter.Day)
                                    {
                                        var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                        listViewConfig.Items.Add(item);
                                    }
                                }
                                //если не используется фильтр по времени
                                else
                                {
                                    var item = new ListViewItem(new[] { 
                                    config.Id.ToString(),
                                    config.Date.ToString()
                                    });
                                    listViewConfig.Items.Add(item);
                                }
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
        //фильтрация по дате
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            //создаем фильтр данных
            dateFilter.Year = dateTimePickerFilter.Value.Year;
            dateFilter.Month = dateTimePickerFilter.Value.Month;
            dateFilter.Day = dateTimePickerFilter.Value.Day;
            useDateFilter = true;
            //если выбрано из списка
            if (comboBoxFavs.SelectedItem != null)
            {
                string favorite = comboBoxFavs.SelectedItem.ToString();
                LoadConfigurationData(favorite);
            }
            //если не выбран из списка, то провести поиск в системе
            else
            {
                //необходимо найти устройство по введенной строке и спросить пользователя, требуется ли подгрузить данные
                string favorite = comboBoxFavs.Text.ToString();
                LoadConfigurationData(favorite);
            }
            useDateFilter = false;
        }
        //сравнение разницы
        private void buttonCompare_Click(object sender, EventArgs e)
        {
            //чтобы сравнить необходимо выбрать две конфигурации
            if (listViewConfig.SelectedItems.Count >= 0)//==2
            {
                var item1 = listViewConfig.SelectedItems[0];
                int configId1 = Int32.Parse(item1.SubItems[0].Text);
                var queryConfig1 = (from c in context.Configs
                                    where c.Id == configId1
                                    select c).FirstOrDefault();

                var item2 = listViewConfig.SelectedItems[1];
                int configId2 = Int32.Parse(item2.SubItems[1].Text);

                var queryConfig2 = (from c in context.Configs
                                    where c.Id == configId2
                                    select c).FirstOrDefault();
                //если конфигурации успешно подгружены
                if (queryConfig1 != null && queryConfig2 != null)
                {
                    string config1 = queryConfig1.Current;
                    string date1 = queryConfig1.Date.ToString();
                    string config2 = queryConfig2.Current;
                    string date2 = queryConfig2.Date.ToString();
                    //отправляем данные на форму                  
                    ConfigDiffer frm = new ConfigDiffer(config1, config2, date1, date2);
                    frm.ShowDialog();
                }
            }
            else
            {
                NotifyInfo("Please choose 2 config for compare!");
            }
        }
    }
}
