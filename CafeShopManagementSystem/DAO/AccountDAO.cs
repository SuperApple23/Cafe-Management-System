using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CafeShopManagementSystem.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            string query = "EXECUTE USP_Login @username , @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }

        public Account GetAccountByUsername(string username)
        {
            string query = "EXECUTE USP_GetAccountByUsername @username";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            foreach (DataRow row in result.Rows)
            {
                return new Account(row);
            }
            return null;
        }

        public DataTable GetListAccount()
        {
            string query = "SELECT username AS N'Username', fullname AS N'Họ tên', phone_number AS N'SDT', email AS N'Email', r.name AS N'Vai trò'\r\nFROM dbo.Accounts AS a\r\nINNER JOIN dbo.Roles AS r\r\n\tON a.role_id = r.id";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result;
        }

        public bool UpdateAccout(string username, string password, string fullname, string phoneNumber, string email, int roleID)
        {
            string query = "UPDATE dbo.Accounts\r\nSET password = @password , fullname = @fullname , phone_number = @phoneNumber , email = @email , role_id = @roleID \r\nWHERE username = @username";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { password, fullname, phoneNumber, email, roleID, username });
            return result > 0;
        }

        public bool UpdatePassword(string username, string newPass)
        {
            string query = "UPDATE dbo.Accounts SET password = @newPass WHERE username = @username";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { newPass, username });
            return result > 0;
        }
    }
}
