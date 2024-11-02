using CafeShopManagementSystem.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        #region Methods
        private bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        private string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        #endregion

        #region Events
        private void ExitClickEvt(object sender, EventArgs e)
        {
            Close();
        }

        private void ClosingEvt(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thực sự muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void LoginClickEvt(object sender, EventArgs e)
        {
            string username = txbUsername.Text.Trim();
            string password = CreateMD5(txbPassword.Text.Trim());

            if(Login(username, password))
            {
                txbPassword.Text = "";
                MainForm homePage = new MainForm();
                homePage.Account = AccountDAO.Instance.GetAccountByUsername(username);
                Hide();
                homePage.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
    }
}
