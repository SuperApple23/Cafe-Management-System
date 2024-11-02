using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class InventoryDAO
    {
        private static InventoryDAO instance;

        public static InventoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private InventoryDAO() { }

        public DataTable GetListInventory()
        {
            string query = "SELECT id AS N'Mã', writer_name AS N'Tên người viết', writing_date AS N'Ngày viết' FROM dbo.Inventories";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public int GetCountInInventory(int id)
        {
            string query = "SELECT COUNT(id) FROM dbo.InventoryInformations WHERE inventory_id = " + id;
            int cnt = (int)DataProvider.Instance.ExecuteScalar(query);

            return cnt;
        }

        public void InsertInventoryInfo(int idInventory, int idGood, double count)
        {
            string query = "EXECUTE USP_InsertInventoryInfo @idInventory , @idGood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idInventory, idGood, count });
        }

        public void InsertInventory()
        {
            string query = "INSERT INTO dbo.Inventories(writer_name) VALUES ('')";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public int GetMaxID()
        {
            string query = "SELECT MAX(id) FROM dbo.Inventories";
            int maxID = (int)DataProvider.Instance.ExecuteScalar(query);
            return maxID;
        }

        public Inventory GetInventoryByID(int id)
        {
            string query = "SELECT * FROM dbo.Inventories";
            Inventory inventory = new Inventory(DataProvider.Instance.ExecuteQuery(query).Rows[0]);

            return inventory;
        }

        public void ReduceGoodInInventory(int idInventory, int idGood)
        {
            string query = "EXECUTE USP_ReduceInventoryInfo @idInventory , @idGood";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idInventory, idGood });
        }

        public bool UpdateInventoryAfterAdd(int id, string writerName)
        {
            string query = "UPDATE dbo.Inventories\r\nSET writer_name = @writerName \r\nWHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { writerName, id });

            return result > 0;
        }

        public DataTable GetInventoryByDate(string dateStart, string dateEnd)
        {
            string query = "SELECT id AS N'Mã', writer_name AS N'Tên người viết', writing_date AS N'Ngày viết' \r\nFROM dbo.Inventories\r\nWHERE writing_date BETWEEN @dateStart AND @dateEnd";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd });

            return result;
        }
    }
}
