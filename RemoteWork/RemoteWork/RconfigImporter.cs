using LinqToExcel;
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
using System.Collections;
using RemoteWork.Data;
using System.Net;

namespace RemoteWork
{

    public partial class RconfigImporter : Form
    {      
        Hashtable network = new Hashtable();
        List<string> uniqList = new List<string>();
        public RconfigImporter()
        {
            InitializeComponent();
            StartConfiguration();
            LoadData();
        }
        private void StartConfiguration()
        {
            numericUpDownPort.Minimum = 22;
            numericUpDownPort.Maximum = 65535;
        }
        private void LoadData()
        {
            using (RconfigContext context = new RconfigContext())
            {
                var queryCredentials = (from c in context.Credentials
                                        select c.CredentialName).ToList();
                comboBoxCredentials.DataSource = queryCredentials;

                var queryCategories = (from c in context.Categories
                                       select c.CategoryName).ToList();
                comboBoxCategories.DataSource = queryCategories;

                var queryProtocols = (from c in context.Protocols
                                      select c.Name).ToList();
                comboBoxProtocols.DataSource = queryProtocols;

                var queryLocations = (from c in context.Locations
                                      select c.LocationName).ToList();
                comboBoxLocations.DataSource = queryLocations;
            }
        }
        //импорт данных из таблицы Excel
        private void ImportFromExcel(string filename)
        {
            try
            {
                var excel = new ExcelQueryFactory();
                excel.FileName = filename;
                //указываем, что первый столбец называется IP Address
                excel.AddMapping<Network>(x => x.Address, "IP Address");
                //на листе Network
                var data = from c in excel.Worksheet<Network>("Network")
                           select c;
                //указываем, что начало обработки данных в указанном диапазоне
                var network = from c in excel.WorksheetRange<Network>("A1", "B1")
                              select c;
                dataGridViewImport.DataSource = data.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //выбор файла для импорта
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files(*.xls)|*.xlsx";
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                //проверить производительность!!!!
                UseWaitCursor = true;
                ImportFromExcel(open.FileName);
                UseWaitCursor = false;

            }
        }
        //импорт данных базу данных 
        private void buttonImport_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            DataAggregate();
            try
            {
                DataImport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
            UseWaitCursor = false;
            if (uniqList.Count > 0)
            {
                string unique = "Not unique addresses and hostnames: " + string.Join(", ", uniqList.ToArray());
                MessageBox.Show(unique, "Unique checking", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //агрегация данных в таблице
        private void DataAggregate()
        {
            foreach (DataGridViewRow row in dataGridViewImport.Rows)
            {
                DataGridViewCell cellAddress = row.Cells["Address"];
                IPAddress address;
                //если значение адреса не пустое, и ip address валидный
                if (!string.IsNullOrWhiteSpace(cellAddress.Value.ToString()) && IPAddress.TryParse(cellAddress.Value.ToString(), out address))
                {
                    DataGridViewCell cellHostname = row.Cells["Hostname"];
                    //если значение имени устройства не пустое
                    if (!string.IsNullOrWhiteSpace(cellHostname.Value.ToString()))
                    {
                        //если в таблице уже существует идентичный адрес, не добавлять
                        if (!network.ContainsKey(cellAddress.Value.ToString()))
                        {
                            //если в таблице существует идентичный устройство, не добавлять
                            if (!network.ContainsValue(cellHostname.Value.ToString()))
                            {
                                network.Add(cellAddress.Value.ToString(), cellHostname.Value.ToString());
                            }
                            else
                            {
                                //указываем, что значение было не уникальным
                                uniqList.Add(cellHostname.Value.ToString());
                            }
                        }
                        else
                        {
                            //указываем, что значение было не уникальным
                            uniqList.Add(cellAddress.Value.ToString());
                        }
                    }
                }
            }
            //clear datagridview
            dataGridViewImport.DataSource = null;
        }
        //импорт в базу данных
        private void DataImport()
        {
            using (RconfigContext context = new RconfigContext())
            {
                bool attributeLoadSuccess = true;
                //данные по местоположению
                string location = comboBoxLocations.SelectedValue.ToString();
                var queryLocation = (from c in context.Locations
                                     where c.LocationName == location
                                     select c).FirstOrDefault();
                if (queryLocation == null)
                    attributeLoadSuccess = false;

                //данные безопасности
                string credential = comboBoxCredentials.SelectedValue.ToString();
                var queryCredential = (from c in context.Credentials
                                       where c.CredentialName == credential
                                       select c).FirstOrDefault();
                if (queryCredential == null)
                    attributeLoadSuccess = false;

                //данные протокола
                string protocol = comboBoxProtocols.SelectedValue.ToString();
                var queryProtocol = (from c in context.Protocols
                                     where c.Name == protocol
                                     select c).FirstOrDefault();
                if (queryProtocol == null)
                    attributeLoadSuccess = false;

                //данные категории
                string category = comboBoxCategories.SelectedValue.ToString();
                var queryCategory = (from c in context.Categories
                                     where c.CategoryName == category
                                     select c).FirstOrDefault();
                if (queryCategory == null)
                    attributeLoadSuccess = false;

                int port = (int)numericUpDownPort.Value;
                //если успешно импортировать устройства
                if (attributeLoadSuccess)
                {
                    foreach (string address in network.Keys)
                    {
                        string hostname = network[address].ToString();

                        Favorite fav = new Favorite();
                        fav.Hostname = hostname;
                        fav.Address = address;
                        fav.Port = port;
                        fav.Protocol = queryProtocol;
                        fav.Location = queryLocation;
                        fav.Credential = queryCredential;
                        fav.Category = queryCategory;
                        fav.Date = DateTime.Now;

                        context.Favorites.Add(fav);
                    }
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Attributes load failed!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }       
    }
}
