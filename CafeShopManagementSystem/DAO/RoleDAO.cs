using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShopManagementSystem.DAO
{
    public class RoleDAO
    {
        private static RoleDAO instance;

        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoleDAO();
                return instance;
            }
            private set { instance = value; }
        }

        private RoleDAO() { }

        public List<Role> GetListRole()
        {
            List<Role> roles = new List<Role>();

            string query = "SELECT * FROM dbo.Roles";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows) 
            { 
                Role role = new Role(row);
                roles.Add(role);
            }

            return roles;
        }

        public string GetRoleByID(int id)
        {
            string query = "SELECT name FROM dbo.Roles WHERE id = " + id;
            string roleName = (string)DataProvider.Instance.ExecuteScalar(query);

            return roleName;
        }
    }
}
