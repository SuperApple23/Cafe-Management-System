using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Receipt
    {
        private int id;
        private string recipitentName;
        private string supplierName;
        private DateTime receiptDate;
        private int confirm;

        public int Id { get => id; set => id = value; }
        public string RecipitentName { get => recipitentName; set => recipitentName = value; }
        public string SupplierName { get => supplierName; set => supplierName = value; }
        public DateTime ReceiptDate { get => receiptDate; set => receiptDate = value; }
        public int Confirm { get => confirm; set => confirm = value; }

        public Receipt() { }

        public Receipt(int id, string recipitentName, string supplierName, DateTime receiptDate, int confirm)
        {
            this.id = id;
            this.recipitentName = recipitentName;
            this.supplierName = supplierName;
            this.receiptDate = receiptDate;
            this.confirm = confirm;
        }

        public Receipt(DataRow row)
        {
            id = (int)row["id"];
            recipitentName = (string)row["recipitent_name"];
            supplierName = (string)row["supplier_name"];
            receiptDate = (DateTime)row["receipt_date"];
            confirm = (int)row["confirm"];
        }
    }
}
