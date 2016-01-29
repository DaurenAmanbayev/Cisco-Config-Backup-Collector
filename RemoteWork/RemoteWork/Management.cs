using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteWork.Managers;

namespace RemoteWork
{
    public partial class Management : Form
    {
        public Management()
        {
            InitializeComponent();
        }

        private void categoryManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category_Manager frm = new Category_Manager();
            frm.MdiParent = this;
            frm.Show();
        }

        private void commandManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command_Manager frm = new Command_Manager();
            frm.MdiParent = this;
            frm.Show();
        }

        private void locationManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Location_Manager frm = new Location_Manager();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
