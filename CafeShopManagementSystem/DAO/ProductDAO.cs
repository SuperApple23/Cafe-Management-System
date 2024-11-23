using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private ProductDAO() { }

        public DataTable GetProductsByCategoryID(int id)
        {
            string query = "SELECT id AS N'Mã', name AS N'Sản phẩm', price AS N'Giá' FROM Products WHERE category_id = " + id + " AND on_sale = 1";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public DataTable GetProductInOrder(int id)
        {
            string query = $"SELECT p.id AS N'Mã', p.name AS N'Sản phẩm', oi.count AS N'Số lượng' FROM dbo.Orders AS o\r\n\tINNER JOIN dbo.OrderInformations as oi\r\n\t\tON o.id = oi.order_id\r\n\tINNER JOIN dbo.Products as p\r\n\t\tON p.id = oi.product_id\r\nWHERE o.id = {id}";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public DataTable GetListProduct()
        {
            string query = "SELECT p.id AS N'Mã', p.name AS N'Tên sản phẩm', description AS N'Mô tả', price AS N'Giá', c.name AS N'Danh mục', on_sale AS N'Đang bán'\r\nFROM dbo.Products AS p\r\nJOIN dbo.Categories AS c\r\n\tON p.category_id = c.id";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public int GetMaxID()
        {
            string query = "SELECT MAX(id) FROM dbo.Products";
            int maxID = (int)DataProvider.Instance.ExecuteScalar(query);
            return maxID;
        }

        public Product GetProductByID(int id)
        {
            string query = "SELECT * FROM dbo.Products WHERE id = @id";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            Product product = new Product(result.Rows[0]);
            return product;
        }

        public string GetThumbnailByID(int id)
        {
            string query = "SELECT thumbnail FROM dbo.Products WHERE id = @id";
            var path = DataProvider.Instance.ExecuteScalar(query, new object[] { id });

            if (path == DBNull.Value)
                return "";
            return (string)path;
        }

        public bool InsertProduct(string name, double price, string thumbnail, string description, int categoryID)
        {
            string query = "INSERT INTO dbo.Products(name, price, thumbnail, description, category_id)\r\nVALUES\r\n( @name , @price , @thumbnail , @description , @categoryID )";
            var thumbnailParam = (!String.IsNullOrEmpty(thumbnail)) ? thumbnail : (object)DBNull.Value;
            var descriptionParam = (!String.IsNullOrEmpty(description)) ? description : (object)DBNull.Value;
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, price, thumbnailParam, descriptionParam, categoryID });

            return result > 0;
        }

        public bool UpdateProduct(int id, string name, double price, string thumbnail, string description, int categoryID)
        {
            string query = "UPDATE dbo.Products\r\nSET name = @name , price = @price , thumbnail = @thumbnail , description = @description , category_id = @categoryID \r\nWHERE id = @id";
            var thumbnailParam = (!String.IsNullOrEmpty(thumbnail)) ? thumbnail : (object)DBNull.Value;
            var descriptionParam = (!String.IsNullOrEmpty(description)) ? description : (object)DBNull.Value;
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] {name, price, thumbnailParam, descriptionParam, categoryID, id});
            
            return result > 0;
        }

        public bool DeleteProduct(int id)
        {
            string queryCount = "SELECT COUNT(id) FROM dbo.OrderInformations WHERE product_id = " + id;
            int cnt = (int)DataProvider.Instance.ExecuteScalar(queryCount);

            int result = 0;
            if (cnt > 0)
            {
                string queryUpdate = "UPDATE dbo.Products SET on_sale = 0, thumbnail = NULL WHERE id = " + id;
                result = DataProvider.Instance.ExecuteNonQuery(queryUpdate);
            }
            else
            {
                string queryDelete = "DELETE FROM dbo.Products WHERE id = " + id;
                result = DataProvider.Instance.ExecuteNonQuery(queryDelete);
            }

            return result > 0;
        }
    }
}
