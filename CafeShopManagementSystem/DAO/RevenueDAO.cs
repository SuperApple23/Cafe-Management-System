using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class RevenueDAO
    {
        private static RevenueDAO instance;

        public static RevenueDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RevenueDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private RevenueDAO() { }

        public double GetRevenue(string dateStart, string dateEnd)
        {
            string query = "SELECT SUM(total) AS total_money FROM \r\n(SELECT o.id, SUM(oi.count * p.price - oi.count * p.price * discount/100) AS 'total', o.order_date\r\nFROM dbo.Orders AS o\r\nINNER JOIN dbo.OrderInformations AS oi\r\n\tON o.id = oi.order_id\r\nINNER JOIN dbo.Products AS p\r\n\tON p.id = oi.product_id\r\nWHERE o.status_id >= 4 AND o.order_date BETWEEN @dateStart AND @dateEnd \r\nGROUP BY o.id, o.order_date) AS total_price";
            var data = DataProvider.Instance.ExecuteScalar(query, new object[] { dateStart, dateEnd });
            double result = (data != DBNull.Value) ? (double)data : 0;

            return result;
        }

        public double GetSpend(string dateStart, string dateEnd)
        {
            string query = "SELECT SUM(total) AS total_money FROM\r\n(SELECT r.id, SUM(ri.count * g.price) AS total, r.receipt_date\r\nFROM dbo.Receipts AS r\r\nINNER JOIN dbo.ReceiptInformations AS ri\r\n\tON r.id = ri.receipt_id\r\nINNER JOIN dbo.Goods AS g\r\n\tON g.id = ri.good_id\r\nWHERE r.confirm = 2 AND r.receipt_date BETWEEN @dateStart AND @dateEnd \r\nGROUP BY r.id, r.receipt_date) AS total_money";
            var data = DataProvider.Instance.ExecuteScalar(query,new object[] { dateStart, dateEnd });
            double result = (data != DBNull.Value) ? (double)data : 0;

            return result;
        }

        public int GetCosume(string dateStart, string dateEnd)
        {
            string query = "SELECT SUM(oi.count) AS total\r\nFROM Orders AS o\r\nINNER JOIN OrderInformations AS oi\r\n\tON o.id = oi.order_id\r\nWHERE o.status_id >= 4 AND o.order_date BETWEEN @dateStart AND @dateEnd";
            var data = DataProvider.Instance.ExecuteScalar(query, new object[] { dateStart, dateEnd });
            int result = (data != DBNull.Value) ? (int)data : 0;

            return result;
        }
    }
}
