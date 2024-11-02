using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class StatusDAO
    {
        private static StatusDAO instance;

        public static StatusDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StatusDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private StatusDAO() { }

        public List<Status> GetListStatus()
        {
            List<Status> statuses = new List<Status>();

            string query = "SELECT * FROM dbo.Status";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Status status = new Status(row);
                statuses.Add(status);
            }

            return statuses;
        }

        public Status GetStatusByOrderID(int orderID)
        {
            string query = "SELECT * FROM dbo.Status WHERE id = @orderID";
            Status status = new Status(DataProvider.Instance.ExecuteQuery(query, new object[] { orderID }).Rows[0]);
            return status;
        }
    }
}
