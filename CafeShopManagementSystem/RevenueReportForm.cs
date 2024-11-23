using CafeShopManagementSystem.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class RevenueReportForm : UserControl
    {
        public RevenueReportForm()
        {
            InitializeComponent();
            LoadRevenue();
            LoadChart(DateTime.Now);
        }

        #region Methods
        public void LoadRevenue()
        {
            DateTime now = DateTime.Now;
            DateTime dateFrom = new DateTime(now.Year, now.Month, 1);
            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);

            double revenue = RevenueDAO.Instance.GetRevenue(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbRevenue.Text = revenue.ToString("C2");

            double spend = RevenueDAO.Instance.GetSpend(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbSpend.Text = spend.ToString("C2");

            int cosume = RevenueDAO.Instance.GetCosume(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbConsume.Text = cosume.ToString() + " sản phẩm";

            double profit = revenue - spend;
            lbProfit.Text = profit.ToString("C2");
        }

        private void LoadChart(DateTime date)
        {
            List<double> revenues = new List<double>();

            for (int month = 1; month <= 12; month++)
            {
                DateTime firstDayOfMonth = new DateTime(date.Year, month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                revenues.Add(RevenueDAO.Instance.GetRevenue(firstDayOfMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd")));
            }

            revenueChart.Series["Doanh thu"].Points.Clear();
            revenueChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;

            for (int month = 0; month < 12; month++)
            {
                string monthString = "T" + (month + 1).ToString();
                revenueChart.Series["Doanh thu"].Points.AddXY(monthString, revenues[month]);
            }
        }
        #endregion

        #region Events
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dtpkChart.Value;
            LoadChart(date);
        }
        #endregion
    }
}
