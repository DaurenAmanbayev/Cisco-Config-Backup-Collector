using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using RemoteWork.Access;
using System.Data.Entity;
using RemoteWork.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace RemoteWork
{   
    //проверка корректности данных
    enum FavoriteInputValidate
    {
       HostnameEmpty,
       HostnameNotUnique,
       AddressEmpty,
       AddressNotParsed,
       AddressNotUnique    
    }
    public partial class Favorite_Edit : Form
    {
        FavoriteInputValidate validateInput = FavoriteInputValidate.HostnameEmpty;
        WindowsMode mode = WindowsMode.ADD;     
        IPAddress address;
        Favorite currentFavorite;
        string prevFavName;
        public Favorite_Edit()
        {
            InitializeComponent();
            StartConfiguration();
            LoadData();
        }

        public Favorite_Edit(string favName)
        {
            InitializeComponent();
            StartConfiguration();           
            prevFavName = favName;
            mode = WindowsMode.EDIT;
            LoadData();
        }

        //начальные настройки формы
        private void StartConfiguration()
        {
            textBoxHostname.MaxLength = 100;
            textBoxAddress.MaxLength = 50;
            numericUpDownPort.Minimum = 22;
            numericUpDownPort.Maximum = 65535;
        }
        //подгружаем данные
        private async void LoadData()
        {
            using (RconfigContext context = new RconfigContext())
            {
                var queryCredentials = await (from c in context.Credentials
                                              select c.CredentialName).ToListAsync();
                comboBoxCredential.DataSource = queryCredentials;

                var queryCategories = await (from c in context.Categories
                                             select c.CategoryName).ToListAsync();
                comboBoxCategory.DataSource = queryCategories;

                var queryProtocols = await (from c in context.Protocols
                                            select c.Name).ToListAsync();
                comboBoxProtocol.DataSource = queryProtocols;

                var queryLocations = await (from c in context.Locations
                                            select c.LocationName).ToListAsync();
                comboBoxLocation.DataSource = queryLocations;
            }
            //если режим редактирования, подгрузить данные избранного
            if (mode == WindowsMode.EDIT)
                LoadPrevData();
        }
        //подгружаем предыдущие данные для редактирования
        private void LoadPrevData()
        {
            using (RconfigContext context = new RconfigContext())
            {
                var queryPrevFavorite = (from c in context.Favorites
                                         where c.Hostname == prevFavName
                                         select c).FirstOrDefault();
                //если избранное было найдено подгружаем данные
                if (queryPrevFavorite != null)
                {
                    //сохраняем наше избранное 
                    currentFavorite = queryPrevFavorite;

                    //редактируем данные формы
                    textBoxHostname.Text = prevFavName;
                    textBoxAddress.Text = queryPrevFavorite.Address;
                    numericUpDownPort.Value = queryPrevFavorite.Port;
                    numericUpDownTimeOut.Value = queryPrevFavorite.TimeOut;

                    //проверить
                    var credential = queryPrevFavorite.Credential.CredentialName;
                    var location = queryPrevFavorite.Location.LocationName;
                    var protocol = queryPrevFavorite.Protocol.Name;
                    var category = queryPrevFavorite.Category.CategoryName;              

                    comboBoxCredential.SelectedItem = credential;
                    comboBoxLocation.SelectedItem = location;
                    comboBoxProtocol.SelectedItem = protocol;                   
                    comboBoxCategory.SelectedItem = category;
                  
                }
                else
                {
                    //если в базе данных не удалось обнаружить избранное с указанным именем, то оставляем форму без изменений
                    mode = WindowsMode.ADD;
                }
            }
        }
        //проверка введенных данных
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxHostname.Text))
            {
                validateInput = FavoriteInputValidate.HostnameEmpty;
                return false;
            }
            else
            {
                //если хост не уникальный вернуть ошибку
                if (!IsUniqueHostname() && mode==WindowsMode.ADD)
                    return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxAddress.Text))
            {
                validateInput = FavoriteInputValidate.AddressEmpty;
                return false;
            }
            else
            {
                if (!IPAddress.TryParse(textBoxAddress.Text.Trim(), out address))
                {
                    validateInput = FavoriteInputValidate.AddressNotParsed;
                    return false;
                }
            }

            
            return true;
        }
        //проверка на уникальность хоста
        private bool IsUniqueHostname()
        {
            bool uniqueHostname = true;          
            string hostname = textBoxHostname.Text.Trim();
            using (RconfigContext context = new RconfigContext())
            {
                context.Favorites.ToList().ForEach(fav =>
                {
                    if (fav.Hostname == hostname)
                    {
                        uniqueHostname = false;
                        validateInput = FavoriteInputValidate.HostnameNotUnique;
                    }

                });
            }
            return uniqueHostname;
        }
        //проверка уникальности адреса
        //не используется!!!
        private bool isUniqueAddress()
        {
            bool uniqueAddress = true;
            string address = textBoxAddress.Text.Trim();
            using (RconfigContext context = new RconfigContext())
            {
                context.Favorites.ToList().ForEach(fav =>
                {
                    if (fav.Address == address)
                    {
                        uniqueAddress = false;
                        validateInput = FavoriteInputValidate.AddressNotUnique;
                    }

                });
            }
            return uniqueAddress;
        }
        //добавляем избранное в базу
        private void FavoriteAdd()
        {
            using (RconfigContext context = new RconfigContext())
            {
                currentFavorite = new Favorite();
                currentFavorite.Hostname = textBoxHostname.Text.Trim();
                currentFavorite.Address = textBoxAddress.Text.Trim();
                currentFavorite.Port = (int)numericUpDownPort.Value;
                currentFavorite.TimeOut=(int)numericUpDownTimeOut.Value;
                currentFavorite.Date = DateTime.UtcNow;
                //данные по местоположению
                string location = comboBoxLocation.SelectedValue.ToString();
                var queryLocation = (from c in context.Locations
                                     where c.LocationName == location
                                     select c).FirstOrDefault();
                if (queryLocation != null)
                    currentFavorite.Location = queryLocation;
                //данные безопасности
                string credential = comboBoxCredential.SelectedValue.ToString();
                var queryCredential = (from c in context.Credentials
                                       where c.CredentialName == credential
                                       select c).FirstOrDefault();
                if (queryCredential != null)
                    currentFavorite.Credential = queryCredential;
                //данные протокола
                string protocol = comboBoxProtocol.SelectedValue.ToString();
                var queryProtocol = (from c in context.Protocols
                                     where c.Name == protocol
                                     select c).FirstOrDefault();
                currentFavorite.Protocol = queryProtocol;
                //данные категории

                string category = comboBoxCategory.SelectedValue.ToString();
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
                                     select c).FirstOrDefault();
                currentFavorite.Category = queryCategory;
                
                //добавляем избранное в базу данных

                context.Favorites.Add(currentFavorite);
                context.SaveChanges();//???
            }
      
        }
        //изменяем избранное из базы
        private void FavoriteEdit()
        {
            using (RconfigContext context = new RconfigContext())
            {
                context.Favorites.Attach(currentFavorite); 
                //данные по категории
                string category = comboBoxCategory.SelectedValue.ToString();
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
                                     select c).FirstOrDefault();
                currentFavorite.Category = queryCategory;
                //-----------------------------------------------------
                currentFavorite.Hostname = textBoxHostname.Text.Trim();
                currentFavorite.Address = textBoxAddress.Text.Trim();
                currentFavorite.Port = (int)numericUpDownPort.Value;
                currentFavorite.TimeOut = (int) numericUpDownTimeOut.Value;
                currentFavorite.Date = DateTime.UtcNow;

                //данные по местоположению
                string location = comboBoxLocation.SelectedValue.ToString();
                var queryLocation = (from c in context.Locations
                                     where c.LocationName == location
                                     select c).FirstOrDefault();
                if (queryLocation != null)
                    currentFavorite.Location = queryLocation;
                //данные безопасности
                string credential = comboBoxCredential.SelectedValue.ToString();
                var queryCredential = (from c in context.Credentials
                                       where c.CredentialName == credential
                                       select c).FirstOrDefault();
                if (queryCredential != null)
                    currentFavorite.Credential = queryCredential;
                //данные протокола
                string protocol = comboBoxProtocol.SelectedValue.ToString();
                var queryProtocol = (from c in context.Protocols
                                     where c.Name == protocol
                                     select c).FirstOrDefault();
                //проверка
                currentFavorite.Protocol = queryProtocol;
              //  MessageBox.Show(queryCategory.CategoryName+ queryProtocol.Name+ queryCredential.CredentialName+ queryLocation.LocationName);
              //  MessageBox.Show("RCONFIG"+currentFavorite.Category.CategoryName);
                //не изменяется проблема!!!!
                //сохраняем изменения                             
                context.Entry(currentFavorite).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
       
        //подтверждаем нашу операцию
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    switch (mode)
                    {
                        case WindowsMode.ADD: FavoriteAdd(); break;
                        case WindowsMode.EDIT: FavoriteEdit(); break;
                    }
                  //  ContextDispose();
                    this.DialogResult = DialogResult.OK;
                }
                catch (DbEntityValidationException dbEx)
                {
                    MessageBox.Show(dbEx.Message);
                   // string report = "";
                    //foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //{
                    //    foreach (var validationError in validationErrors.ValidationErrors)
                    //    {
                    //       Trace.TraceInformation("Property: {0} Error: {1}",
                    //                                validationError.PropertyName,
                    //                                validationError.ErrorMessage);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                switch (validateInput)
                {
                    case FavoriteInputValidate.HostnameEmpty: NotifyWarning("Hostname is empty or whitespace!"); break;
                    case FavoriteInputValidate.HostnameNotUnique: NotifyWarning("Hostname is not unique!"); break;
                    case FavoriteInputValidate.AddressEmpty: NotifyWarning("IP Address is empty or whitespace"); break;
                    case FavoriteInputValidate.AddressNotParsed: NotifyWarning("IP Address not right!"); break;
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
        //смена порта на порт по умолчанию для выбранного протокола
        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (RconfigContext context = new RconfigContext())
            {
                //подгружаем данные о портах по умолчанию для выбранного протокола
                var queryPort = (from c in context.Protocols
                                 where c.Name == comboBoxProtocol.SelectedValue.ToString()
                                 select c).FirstOrDefault();
                if (queryPort != null)
                    numericUpDownPort.Value = queryPort.DefaultPort;
            }
        }
        
    }
}
