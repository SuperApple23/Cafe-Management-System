using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Order
    {
        private int id;
        private DateTime orderDate;
        private string note;
        private string buyerName;
        private string address;
        private float discount;
        private float totalCost;
        private int statusID;
        private int shippingMethodID;
        private int paymentMethodID;
        private float moneyReceived;

        public int Id { get => id; set => id = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public string Note { get => note; set => note = value; }
        public string BuyerName { get => buyerName; set => buyerName = value; }
        public string Address { get => address; set => address = value; }
        public float Discount { get => discount; set => discount = value; }
        public float TotalCost { get => totalCost; set => totalCost = value; }
        public int StatusID { get => statusID; set => statusID = value; }
        public int ShippingMethodID { get => shippingMethodID; set => shippingMethodID = value; }
        public int PaymentMethodID { get => paymentMethodID; set => paymentMethodID = value; }
        public float MoneyReceived { get => moneyReceived; set => moneyReceived = value; }

        public Order() { }

        public Order(int id, DateTime orderDate, string note, string buyerName, string address, float discount, float totalCost, int statusID, int shippingMethodID, int paymentMethodID, float moneyReceived)
        {
            this.id = id;
            this.orderDate = orderDate;
            this.note = note;
            this.buyerName = buyerName;
            this.address = address;
            this.discount = discount;
            this.totalCost = totalCost;
            this.statusID = statusID;
            this.shippingMethodID = shippingMethodID;
            this.paymentMethodID = paymentMethodID;
            this.moneyReceived = moneyReceived;
        }

        public Order(DataRow row)
        {
            id = (int)row["id"];
            orderDate = (DateTime)row["order_date"];
            note = row["note"] != DBNull.Value ? (string)row["note"] : "";
            buyerName = row["buyer_name"] != DBNull.Value ? (string)row["buyer_name"] : "";
            address = row["address"] != DBNull.Value ? (string)row["address"] : "";
            discount = Convert.ToSingle((double)row["discount"]);
            totalCost = Convert.ToSingle((double)row["total_cost"]);
            statusID = (int)row["status_id"];
            shippingMethodID = (int)row["shipping_method_id"];
            paymentMethodID = (int)row["payment_method_id"];
            moneyReceived = Convert.ToSingle((double)row["money_received"]);
        }
    }
}
