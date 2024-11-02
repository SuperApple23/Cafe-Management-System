using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem.DAO
{
    public class ReceiptDAO
    {
        private static ReceiptDAO instance;

        public static ReceiptDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReceiptDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private ReceiptDAO() { }

        public DataTable GetListReceipt()
        {
            string query = "SELECT id AS N'Mã', recipitent_name AS N'Bên bán', supplier_name AS N'Người nhận', receipt_date AS N'Ngày nhận', confirm AS N'Xác nhận' FROM dbo.Receipts";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public Receipt GetReceiptByID(int id)
        {
            string query = "SELECT * FROM dbo.Receipts WHERE id = " + id;
            Receipt receipt = new Receipt(DataProvider.Instance.ExecuteQuery(query).Rows[0]);

            return receipt;
        }

        public int GetCountInReceipt(int id)
        {
            string query = "SELECT COUNT(id) FROM dbo.ReceiptInformations WHERE receipt_id = " + id;
            int cnt = (int)DataProvider.Instance.ExecuteScalar(query);

            return cnt;
        }

        public void InsertReceiptInfo(int idReceipt, int idGood, double count)
        {
            string query = "EXECUTE USP_InsertReceiptInfo @idReceipt , @idGood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idReceipt, idGood, count });
        }

        public void InsertReceipt()
        {
            string query = "INSERT INTO dbo.Receipts(recipitent_name, supplier_name) VALUES ('', '')";
            DataProvider.Instance.ExecuteQuery(query);
        }

        public bool UpdateReceiptAfterAdd(int id, string recipitentName, string supplierName)
        {
            string query = "UPDATE dbo.Receipts\r\nSET recipitent_name = @recipitentName , supplier_name = @supplierName , confirm = 1\r\nWHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { recipitentName, supplierName, id });

            return result > 0;
        }

        public int GetMaxID()
        {
            string query = "SELECT MAX(id) FROM dbo.Receipts";
            int maxID = (int)DataProvider.Instance.ExecuteScalar(query);
            return maxID;
        }

        public void ReduceGoodInReceipt(int idReceipt, int idGood)
        {
            string query = "EXECUTE USP_ReduceReceiptInfo @idReceipt , @idGood";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idReceipt, idGood });
        }

        public bool UpdateReceipt(int id, string recipitentName, string supplierName)
        {
            string query = "UPDATE dbo.Receipts\r\nSET recipitent_name = @recipitentName , supplier_name = @supplierName \r\nWHERE id = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { recipitentName, supplierName, id });

            return result > 0;
        }

        public bool DeleteReceipt(int id)
        {
            int result = 0;
            try
            {
                string queryReceiptInfo = "DELETE FROM dbo.ReceiptInformations WHERE receipt_id = @id";
                string queryReceipt = "DELETE FROM dbo.Receipts WHERE id = @id";

                DataProvider.Instance.ExecuteNonQuery(queryReceiptInfo, new object[] { id });
                result = DataProvider.Instance.ExecuteNonQuery(queryReceipt, new object[] { id });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
            }
            return result >= 1;
        }

        public void ConfirmReceipt(int id)
        {
            string query = "UPDATE dbo.Receipts SET confirm = 2 WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public DataTable GetReceiptByDate(string dateStart , string dateEnd)
        {
            string query = "SELECT id AS N'Mã', recipitent_name AS N'Bên bán', supplier_name AS N'Người nhận', receipt_date AS N'Ngày nhận', confirm AS N'Xác nhận'\r\nFROM dbo.Receipts\r\nWHERE receipt_date BETWEEN @dateStart AND @dateEnd";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd });

            return result;
        }
    }
}
