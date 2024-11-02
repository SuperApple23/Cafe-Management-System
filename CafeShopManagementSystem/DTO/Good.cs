using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Good
    {
        private int id;
        private string goodName;
        private float price;
        private int type;
        private int isActive;

        public int Id { get => id; set => id = value; }
        public string GoodName { get => goodName; set => goodName = value; }
        public float Price { get => price; set => price = value; }
        public int Type { get => type; set => type = value; }
        public int IsActive { get => isActive; set => isActive = value; }

        public Good() { }

        public Good(int id, string goodName, float price, int type, int isActive)
        {
            this.id = id;
            this.goodName = goodName;
            this.price = price;
            this.type = type;
            this.isActive = isActive;
        }

        public Good(DataRow row)
        {
            id = (int)row["id"];
            goodName = (string)row["good_name"];
            price = Convert.ToSingle((double)row["price"]);
            type = (int)row["type"];
            isActive = (int)row["is_active"];
        }
    }
}
