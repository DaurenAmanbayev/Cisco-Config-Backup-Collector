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
    enum FavoriteInputValidate
    {
       HostnameEmpty,
       HostnameNotUnique,
       AddressEmpty,
       AddressNotParsed      
    }
    public partial class Favorite_Edit : Form
    {
        FavoriteInputValidate validateInput = FavoriteInputValidate.HostnameEmpty;
        WindowsMode mode = WindowsMode.ADD;
        RconfigContext context = new RconfigContext();
        IPAddress address;
        public Favorite_Edit()
        {
            InitializeComponent();
            StartConfiguration();
            LoadData();
        }
        private void StartConfiguration()
        {
            textBoxHostname.MaxLength = 100;
            textBoxAddress.MaxLength = 50;
            numericUpDownPort.Minimum = 22;
            numericUpDownPort.Maximum = 65535;
        }
        private async void LoadData()
        {
            var queryCredentials = await (from c in context.Credentials
                                   select c.CredentialName).ToListAsync();
            comboBoxCredential.DataSource = queryCredentials;

            var queryCategories=await (from c in context.Categories
                                select c.CategoryName).ToListAsync();
            comboBoxCategory.DataSource = queryCategories;

            var queryProtocols =await (from c in context.Protocols
                               select c.Name).ToListAsync();
            comboBoxProtocol.DataSource = queryProtocols;

            var queryLocations = await (from c in context.Locations
                                        select c.LocationName).ToListAsync();
            comboBoxLocation.DataSource = queryLocations;

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
                if (!IsUniqueHostname())
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
        private bool IsUniqueHostname()
        {
            bool uniqueHostname = true;          
            string hostname = textBoxHostname.Text.Trim();           
            context.Favorites.ToList().ForEach(fav =>
            {
                if (fav.Hostname == hostname)
                {
                    uniqueHostname = false;
                    validateInput = FavoriteInputValidate.HostnameNotUnique;
                }                

            });
            return uniqueHostname;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    Favorite fav = new Favorite();
                    fav.Hostname = textBoxHostname.Text.Trim();
                    fav.Address = textBoxAddress.Text.Trim();
                    fav.Port = (int)numericUpDownPort.Value;
                    fav.Date = DateTime.UtcNow;
                    //данные по местоположению
                    string location = comboBoxLocation.SelectedValue.ToString();
                    var queryLocation = (from c in context.Locations
                                         where c.LocationName == location
                                         select c).FirstOrDefault();
                    if (queryLocation != null)
                        fav.Location = queryLocation;
                    //данные безопасности
                    string credential = comboBoxCredential.SelectedValue.ToString();
                    var queryCredential = (from c in context.Credentials
                                           where c.CredentialName == credential
                                           select c).FirstOrDefault();
                    if (queryCredential != null)
                        fav.Credential = queryCredential;
                    //данные протокола
                    string protocol = comboBoxProtocol.SelectedValue.ToString();
                    var queryProtocol = (from c in context.Protocols
                                         where c.Name == protocol
                                         select c).FirstOrDefault();
                    fav.Protocol = queryProtocol;
                    //данные категории
                    string category = comboBoxCategory.SelectedValue.ToString();
                    var queryCategory = (from c in context.Categories
                                         where c.CategoryName == category
                                         select c).FirstOrDefault();
                    if (queryCategory != null)
                        queryCategory.Favorites.Add(fav);
                    //добавляем избранное в базу данных

                    context.Favorites.Add(fav);
                    context.SaveChanges();//???

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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void NotifyWarning(string warning)
        {
            MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //подгружаем данные о портах по умолчанию для выбранного протокола
            var queryPort=(from c in context.Protocols
                          where c.Name==comboBoxProtocol.SelectedValue.ToString()
                          select c).FirstOrDefault();
            if (queryPort != null)
                numericUpDownPort.Value = queryPort.DefaultPort;
        }
        
    }
}
