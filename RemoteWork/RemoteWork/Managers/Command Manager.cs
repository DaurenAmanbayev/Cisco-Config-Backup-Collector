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
    public partial class Command_Manager : Form
    {
        RconfigContext context = new RconfigContext();

        public Command_Manager()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        { 
            var queryCommands=await (from c in context.Commands
                              select c.Name).ToListAsync();

            listBoxCommands.DataSource = queryCommands;
        }

        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_Edit frm = new Command_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void editCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_Edit frm = new Command_Edit();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void deleteCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
