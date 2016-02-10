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

namespace RemoteWork.Managers
{
    public partial class Location_Manager : Form
    {
        RconfigContext context = new RconfigContext();
        public Location_Manager()
        {
            InitializeComponent();
            LoadData();
        }
        //подгрузка данных
        public async void LoadData()
        {
            var queryLocations = await (from c in context.Locations
                                 select c.LocationName).ToListAsync();

            listBoxLocations.DataSource = queryLocations;
        }
        //добавить местоположение
        private void addLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Location_Edit frm = new Location_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }
        //редактировать местоположение
        private void editLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItem != null)
            {
                string location = listBoxLocations.SelectedItem.ToString();
                Location_Edit frm = new Location_Edit(location);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            
        }
        //удаление
        private void deleteLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxLocations.SelectedItem != null)
            {
                string location = listBoxLocations.SelectedItem.ToString();

                var queryLocation=(from c in context.Locations
                                  where c.LocationName==location
                                  select c).FirstOrDefault();
                if (queryLocation != null)
                {
                    context.Locations.Remove(queryLocation);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }
    }
}
