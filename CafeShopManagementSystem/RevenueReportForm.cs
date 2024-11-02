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
        }

        public void LoadRevenue()
        {
            DateTime now = DateTime.Now;
            DateTime dateFrom = new DateTime(now.Year, now.Month, 1);
            DateTime dateTo = now.AddMonths(1).AddDays(-1);

            double revenue = RevenueDAO.Instance.GetRevenue(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbRevenue.Text = revenue.ToString("C2");

            double spend = RevenueDAO.Instance.GetSpend(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbSpend.Text = spend.ToString("C2");

            int cosume = RevenueDAO.Instance.GetCosume(dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd"));
            lbConsume.Text = cosume.ToString() + " sản phẩm";

            double profit = revenue - spend;
            lbProfit.Text = profit.ToString("C2");
        }
    }
}
