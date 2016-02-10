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
        bool useFailedFilter = false;
        bool useDateFilter = false;
        string dateFilter;
        public Analytic()
        {
            InitializeComponent();
        }
        //вызов отчетности
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
        //подгрузка данных
        private void LoadData()
        {
            var queryReport = from c in context.Reports
                                     select c;

            var filterQuery = queryReport;
            if (useFailedFilter)
            {
                filterQuery = from c in queryReport
                              where c.Status==false
                              select c;
            }
            if (useDateFilter)
            {
                filterQuery = from c in filterQuery
                              where c.Date.Value.ToShortDateString() == dateFilter
                              select c;
            }
                
            dataGridViewReports.DataSource = filterQuery.ToListAsync();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            dateFilter = dateTimePickerFilter.Value.ToShortDateString();
            useDateFilter = true;
            LoadData();
            useDateFilter = false;
        }

        private void checkBoxFailed_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxFailed.CheckState == CheckState.Checked)
            {
                useFailedFilter = true;
            }
            else if (checkBoxFailed.CheckState == CheckState.Unchecked)
            {
                useFailedFilter = false;
            }
        }

        //draft....
        //datagrid fill query
        //var query = from c in context.Outcome
        //            select new
        //            {
        //                outcoming_id = c.outcom_id,
        //                outcoming_item = c.Costs_item.cost_name,
        //                outcoming_category = c.Costs_item.Category.cat_name,
        //                outcoming_operation = c.Oper_type.type_name,
        //                outcoming_cost = c.item_cost,
        //                outcoming_count = c.item_count,
        //                outcoming_summary = c.item_count * c.item_cost,
        //                outcoming_date = c.outcom_date,
        //                outcoming_notes = c.notes
        //            };
    }
}
