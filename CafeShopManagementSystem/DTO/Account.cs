using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DTO
{
    public class Account : ICloneable
    {
        private string username;
        private string password;
        private string fullname;
        private string phoneNumber;
        private string email;
        private int roleID;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public int RoleID { get => roleID; set => roleID = value; }

        public Account() { }

        public Account(string username, string password, string fullname, string phoneNumber, string email, int roleID)
        {
            this.username = username;
            this.password = password;
            this.fullname = fullname;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.roleID = roleID;
        }

        public Account(DataRow row)
        {
            username = (string)row["username"];
            password = (string)row["password"];
            fullname = (string)row["fullname"];
            phoneNumber = (string)row["phone_number"];
            email = (string)row["email"];
            roleID = (int)row["role_id"];
        }

        public object Clone()
        {
            return new Account(username, password, fullname, phoneNumber, email, roleID);
        }
    }
}
