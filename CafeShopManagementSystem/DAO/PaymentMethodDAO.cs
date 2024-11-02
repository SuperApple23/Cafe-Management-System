using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class PaymentMethodDAO
    {
        private static PaymentMethodDAO instance;

        public static PaymentMethodDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new PaymentMethodDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private PaymentMethodDAO() { }

        public List<PaymentMethod> GetListPaymentMethod()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            string query = "SELECT * FROM dbo.PaymentMethod";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                PaymentMethod pm = new PaymentMethod(row);
                paymentMethods.Add(pm);
            }

            return paymentMethods;
        }
    }
}
