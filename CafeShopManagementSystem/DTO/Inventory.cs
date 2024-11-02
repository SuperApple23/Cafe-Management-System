using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Inventory
    {
        private int id;
        private string writerName;
        private DateTime writingDate;

        public int Id { get => id; set => id = value; }
        public string WriterName { get => writerName; set => writerName = value; }
        public DateTime WritingDate { get => writingDate; set => writingDate = value; }

        public Inventory() { }

        public Inventory(int id, string writerName, DateTime writingDate)
        {
            this.id = id;
            this.writerName = writerName;
            this.writingDate = writingDate;
        }

        public Inventory(DataRow row)
        {
            id = (int)row["id"];
            writerName = (string)row["writer_name"];
            writingDate = (DateTime)row["writing_date"];
        }
    }
}
