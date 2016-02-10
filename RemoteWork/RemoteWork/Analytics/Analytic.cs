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

namespace RemoteWork.Analytics
{
    public partial class Analytic : Form
    {
        RconfigContext context = new RconfigContext();
        public Analytic()
        {
            InitializeComponent();
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LoadData()
        {
            var queryReport = await (from c in context.Reports
                                     select c).ToListAsync();
            dataGridViewReports.DataSource = queryReport;
        }
    }
}
