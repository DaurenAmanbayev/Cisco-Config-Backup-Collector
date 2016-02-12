using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWork
{
   
    public partial class RconfigImporter : Form
    {

        public RconfigImporter()
        {
            InitializeComponent();
            //StartConfiguration();
        }
        private void StartConfiguration()
        {
            List<string> import = new List<string>
            {
                "Hostname and Address",
                "Favorite and Network",
                "Favorite and Category",
                "Favorite and Location"
            };
            //toolStripComboBoxImportType.ComboBox.DataSource = import;
        }
        private void Import(string filename)
        {
            var excel = new ExcelQueryFactory();
            excel.FileName = filename;

            excel.AddMapping<Network>(x => x.Address, "IP Address");
            var data = from c in excel.Worksheet<Network>("Network")
                       select c;


            var network = from c in excel.WorksheetRange<Network>("A1", "B1")
                          select c;
            dataGridViewImport.DataSource = data.ToList();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel Files(*.xls)|*.xlsx";
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                //проверить производительность!!!!
                UseWaitCursor = true;
                //MessageBox.Show(open.FileName);
                //Task task = Task.Factory.StartNew(() => Import(open.FileName));
               // task.Wait();
                Import(open.FileName);
                UseWaitCursor = false;
               
            }
        }
      
    }
}
