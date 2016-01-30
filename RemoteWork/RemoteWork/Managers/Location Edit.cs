using RemoteWork.Access;
using RemoteWork.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWork.Managers
{
    enum LocationInputValidate
    {
        LocationEmpty,
        LocationNotUnique
    }
    public partial class Location_Edit : Form
    {
        WindowsMode mode = WindowsMode.ADD;
        LocationInputValidate validateInput = LocationInputValidate.LocationEmpty;
        RconfigContext context = new RconfigContext();
        string prevLocation;
        Location currentLocation;
        public Location_Edit()
        {
            InitializeComponent();
        }

        public Location_Edit(string location)
        {
            InitializeComponent();
            prevLocation = location;
            textBoxLocation.Text = location;
            mode = WindowsMode.EDIT;
            LoadData();
        }

        private void LoadData()
        {
            var queryLocation=(from c in context.Locations
                              where c.LocationName==prevLocation
                              select c).FirstOrDefault();
            if (queryLocation != null)
                currentLocation = queryLocation;
        }

        private bool isUnique(string location)
        {
            //если режим редактирования, то проверка на уникальность должна учитывать существующее имя
            if (mode == WindowsMode.EDIT && prevLocation == textBoxLocation.Text.Trim())
            {
                return true;
            }
            bool uniqueLocation = true;
            context.Locations.ToList().ForEach(loc =>
            {
                if (loc.LocationName == location)
                {
                    uniqueLocation = false;
                    validateInput = LocationInputValidate.LocationNotUnique;                  
                }

            });

            return uniqueLocation;
        }

        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxLocation.Text.Trim()))
            {
                return false;
            }            
            return isUnique(textBoxLocation.Text.Trim());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                switch (mode)
                {
                    case WindowsMode.ADD:
                        currentLocation = new Location();
                        currentLocation.LocationName = textBoxLocation.Text.Trim();
                        context.Locations.Add(currentLocation);
                        context.SaveChanges();
                        break;
                    case WindowsMode.EDIT:
                        currentLocation.LocationName = textBoxLocation.Text.Trim();
                        context.Entry(currentLocation).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        break;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validateInput)
                {
                    case LocationInputValidate.LocationEmpty: NotifyWarning("Location input is empty!"); break;
                    case LocationInputValidate.LocationNotUnique: NotifyWarning("Location is not unique!"); break;
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
    }
}
