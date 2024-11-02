using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CafeShopManagementSystem.DAO
{
    public class ShippingMethodDAO
    {
        private static ShippingMethodDAO instance;

        public static ShippingMethodDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ShippingMethodDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private ShippingMethodDAO() { }

        public List<ShippingMethod> GetListShippingMethod()
        {
            List<ShippingMethod> shippingMethods = new List<ShippingMethod>();

            string query = "SELECT * FROM dbo.ShippingMethod";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                ShippingMethod sm = new ShippingMethod(row);
                shippingMethods.Add(sm);
            }

            return shippingMethods;
        }
    }
}
