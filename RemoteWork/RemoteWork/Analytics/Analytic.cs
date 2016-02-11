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
    //структура для фильтрации даты
    public struct DateFilter
    {
        public int Year;
        public int Month;
        public int Day;
    }
    public partial class Analytic : Form
    {
        RconfigContext context=new RconfigContext();
        bool useFailedFilter = false;
        bool useDateFilter = false;
        DateFilter dateFilter = new DateFilter();
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
            //nested query
            var filterQuery = queryReport;
            if (useFailedFilter)//фильтр неудавшихся задач
            {
                filterQuery = from c in queryReport
                              where c.Status == false
                              select c;
            }
            if (useDateFilter)//фильтр по дате
            {
                filterQuery = from c in filterQuery
                              where c.Date.Value.Year == dateFilter.Year    
                               & c.Date.Value.Month==dateFilter.Month
                               & c.Date.Value.Day==dateFilter.Day
                              select c;                         
            }

            dataGridViewReports.DataSource = filterQuery.ToList();

        }
        //фильтр по дате
        private void buttonFilter_Click(object sender, EventArgs e)
        {
            //создаем фильтр данных
            dateFilter.Year= dateTimePickerFilter.Value.Year;
            dateFilter.Month = dateTimePickerFilter.Value.Month;
            dateFilter.Day = dateTimePickerFilter.Value.Day;
            //указываем, что требуется использование фильтров
            useDateFilter = true;
            LoadData();
            //сбрасываем наш фильтр
            useDateFilter = false;
        }
        //проверка неудавшихся задач 
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
