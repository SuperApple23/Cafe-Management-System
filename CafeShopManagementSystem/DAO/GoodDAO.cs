using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class GoodDAO
    {
        private static GoodDAO instance;

        public static GoodDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GoodDAO();
                return instance;
            }
            private set { instance = value; }

        }

        private GoodDAO() { }

        public DataTable GetListGood()
        {
            string query = "SELECT id AS N'Mã', good_name AS N'Hàng hóa', price AS N'Giá', type AS N'Loại', is_active AS N'Đang dùng' FROM dbo.Goods";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public DataTable GetGoodInReceipt(int receiptID)
        {
            string query = "SELECT good_id AS N'Mã', good_name AS N'Hàng hóa', count AS N'Số lượng'\r\nFROM dbo.ReceiptInformations AS ri\r\nINNER JOIN dbo.Goods AS g\r\n\tON ri.good_id = g.id\r\nWHERE receipt_id = " + receiptID;
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public DataTable GetGoodInInventory(int inventoryID)
        {
            string query = "SELECT good_id AS N'Mã', good_name AS N'Hàng hóa', count AS N'Số lượng'\r\nFROM dbo.InventoryInformations AS ii\r\nINNER JOIN dbo.Goods AS g\r\n\tON ii.good_id = g.id\r\nWHERE inventory_id = " + inventoryID;
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public List<Good> GetListGoodIsActive()
        {
            List<Good> goods = new List<Good>();

            string query = "SELECT * FROM dbo.Goods WHERE is_active = 1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow row in data.Rows)
            {
                Good good = new Good(row);
                goods.Add(good);
            }

            return goods;
        }

        public Good GetGoodByID(int id)
        {
            string query = "SELECT * FROM dbo.Goods WHERE id = @id";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            Good good = new Good(result.Rows[0]);
            return good;
        }

        public bool InsertGood(string name, double price, int type)
        {
            string query = "INSERT INTO dbo.Goods(good_name, price, type)\r\nVALUES\r\n( @name , @price , @type )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, price, type });

            return result > 0;
        }

        public bool UpdateGood(int id, string name, double price, int type)
        {
            string query = "UPDATE dbo.Goods SET good_name = @name , price = @price , type = @type WHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, price, type, id });
            
            return result > 0;
        }

        public bool DeleteGood(int id)
        {
            string queryCount = "SELECT COUNT(id) FROM dbo.ReceiptInformations WHERE good_id = " + id;
            int cnt = (int)DataProvider.Instance.ExecuteScalar(queryCount);

            int result = 0;
            if (cnt > 0)
            {
                string queryUpdate = "UPDATE dbo.Goods SET is_active = 0 WHERE id = " + id;
                result = DataProvider.Instance.ExecuteNonQuery(queryUpdate);
            }
            else
            {
                string queryDelete = "DELETE FROM dbo.Goods WHERE id = " + id;
                result = DataProvider.Instance.ExecuteNonQuery(queryDelete);
            }

            return result > 0;
        }

        public DataTable SearchGoodByName(string name)
        {
            string query = $"SELECT id AS N'Mã', good_name AS N'Hàng hóa', price AS N'Giá', type AS N'Loại', is_active AS N'Đang dùng' FROM dbo.Goods WHERE good_name LIKE N'%{name}%'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }
    }
}
