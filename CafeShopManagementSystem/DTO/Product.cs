using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Product
    {
        private int id;
        private string name;
        private float price;
        private string thumbnail;
        private string description;
        private int categoryID;
        private int onSale;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Description { get => description; set => description = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public int OnSale { get => onSale; set => onSale = value; }

        public Product() { }

        public Product(int id, string name, float price, string thumbnail, string description, int categoryID, int onSale)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.thumbnail = thumbnail;
            this.description = description;
            this.categoryID = categoryID;
            this.onSale = onSale;
        }

        public Product(DataRow row)
        {
            id = (int)row["id"];
            name = (string)row["name"];
            price = Convert.ToSingle((double)row["price"]);
            thumbnail = row["thumbnail"] != DBNull.Value ? (string)row["thumbnail"] : "";
            description = row["description"] != DBNull.Value ? (string)row["description"] : "";
            categoryID = (int)row["category_id"];
            onSale = (int)row["on_sale"];
        }
    }
}
