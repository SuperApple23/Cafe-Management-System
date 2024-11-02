using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private OrderDAO() { }

        public void InsertOrder()
        {
            string query = "INSERT INTO dbo.Orders(order_date, status_id) VALUES (DEFAULT, DEFAULT)";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public int GetMaxID()
        {
            string query = "SELECT MAX(id) FROM dbo.Orders";
            int maxID = (int)DataProvider.Instance.ExecuteScalar(query);
            return maxID;
        }

        public void InsertOrderInfo(int idOrder, int idProduct, int count)
        {
            string query = "EXECUTE USP_InsertOrderInfo @idOrder , @idProduct ,  @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idOrder, idProduct, count });
        }

        public DataTable GetListOrder()
        {
            string query = "SELECT o.id AS N'Mã', order_date AS N'Ngày đặt hàng', total_cost AS N'Tổng tiền', s.name AS N'Trạng thái', sm.name AS N'Loại'\r\nFROM dbo.Orders AS o \r\nINNER JOIN dbo.Status AS s \r\n\tON o.status_id = s.id\r\nINNER JOIN dbo.ShippingMethod AS sm\r\n\tON o.shipping_method_id = sm.id";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public Order GetOrderByID(int id)
        {
            string query = "SELECT * FROM dbo.Orders WHERE id = " + id;
            Order order = new Order(DataProvider.Instance.ExecuteQuery(query).Rows[0]);

            return order;
        }

        public double CalculateTotalMoney(int id)
        {
            string query = "SELECT SUM(oi.count * p.price) AS N'Total money'\r\nFROM OrderInformations AS oi\r\nJOIN Products as p\r\n\tON oi.product_id = p.id\r\nWHERE order_id = @id";
            double totalMoney = 0;
            if (DataProvider.Instance.ExecuteScalar(query, new object[] { id }) != DBNull.Value)
            {
                totalMoney = Convert.ToDouble(DataProvider.Instance.ExecuteScalar(query, new object[] { id }));
            }

            return totalMoney;
        }

        public void UpdateOrderAfterPayment(string note, string buyerName, string address, double discount, double totalCost, int smID, int pmID, double moneyReceived, int id)
        {
            string query = "UPDATE dbo.Orders\r\nSET note = @note , buyer_name = @buyerName , address = @address , discount = @discount , total_cost = @totalCost , status_id = 2, shipping_method_id = @smID , payment_method_id = @pmID , money_received = @moneyReceiced \r\nWHERE id = @id";
            var noteParam = (!String.IsNullOrEmpty(note)) ? note : (object)DBNull.Value;
            var buyerNameParam = (!String.IsNullOrEmpty(buyerName)) ? buyerName : (object)DBNull.Value;
            var addressParam = (!String.IsNullOrEmpty(address)) ? address : (object)DBNull.Value;

            DataProvider.Instance.ExecuteNonQuery(query, new object[] { noteParam, buyerNameParam, addressParam, discount, totalCost, smID, pmID, moneyReceived, id });
        }

        public bool UpdateOrder(string note, string buyerName, string address, int statusID, int smID, int id)
        {
            string query = "UPDATE dbo.Orders\r\nSET note = @note , buyer_name = @buyerName , address = @address , status_id = @statusID , shipping_method_id = @smID \r\nWHERE id = @id";
            var noteParam = (!String.IsNullOrEmpty(note)) ? note : (object)DBNull.Value;
            var buyerNameParam = (!String.IsNullOrEmpty(buyerName)) ? buyerName : (object)DBNull.Value;
            var addressParam = (!String.IsNullOrEmpty(address)) ? address : (object)DBNull.Value;

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { noteParam, buyerNameParam, addressParam, smID, statusID, id });

            return result >= 1;
        }

        public bool DeleteOrder(int id)
        {
            int result = 0;
            try
            {
                string queryOrderInfo = "DELETE FROM dbo.OrderInformations WHERE order_id = @id";
                string queryOrder = "DELETE FROM dbo.Orders WHERE id = @id";

                DataProvider.Instance.ExecuteNonQuery(queryOrderInfo, new object[] { id });
                result = DataProvider.Instance.ExecuteNonQuery(queryOrder, new object[] { id });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
            }
            return result >= 1;
        }

        public void ReduceProductInOrder(int idOrder, int idProduct)
        {
            string query = "EXECUTE USP_ReduceOrderInfo @idOrder , @idProduct";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idOrder, idProduct });
        }
    }
}
