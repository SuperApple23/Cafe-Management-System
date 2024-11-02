using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class AccountForm : UserControl
    {
        public AccountForm()
        {
            InitializeComponent();
            LoadAccounts();
            LoadRoles();
        }

        #region Methods
        private void LoadAccounts()
        {
            dtgvAccount.DataSource = AccountDAO.Instance.GetListAccount();
        }

        private void LoadRoles()
        {
            cbRole.DataSource = RoleDAO.Instance.GetListRole();
            cbRole.DisplayMember = "name";
        }
        #endregion

        #region Events
        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvAccount.SelectedRows.Count > 0)
            {
                btnUpdateAccount.Enabled = true;

                string username = (string)dtgvAccount.SelectedRows[0].Cells[0].Value;
                Account account = AccountDAO.Instance.GetAccountByUsername(username);

                txbUsername.Text = account.Username;
                txbPassword.Text = account.Password;
                txbFullname.Text = account.Fullname;
                txbPhoneNumber.Text = account.PhoneNumber;
                txbEmail.Text = account.Email;

                cbRole.SelectedIndex = account.RoleID - 1;
            }
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text.Trim();
            string fullname = txbFullname.Text.Trim();
            string email = txbEmail.Text.Trim();
            string password = txbPassword.Text.Trim();
            string phoneNumber = txbPhoneNumber.Text.Trim();
            int roleID = (cbRole.SelectedItem as Role).ID;

            Account acc = AccountDAO.Instance.GetAccountByUsername(username);

            Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regexEmail.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Email sai định dạng: abc@xyz.mnl", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbEmail.Focus();
                return;
            }

            if (password != acc.Password)
            {
                Regex regexPassword = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.#?!@$%^&*-]).{8,}$");
                match = regexPassword.Match(password);
                if (!match.Success)
                {
                    string message = "Mật khẩu sai định dạng:\n" +
                                    "- Phải có ít nhất 1 chữ hoa\n" +
                                    "- Phải có ít nhất 1 chữ thường\n" +
                                    "- Phải có ít nhất 1 chữ số\n" +
                                    "- Phải có ít nhất 1 kí tự [.#?!@$%^&*-]\n" +
                                    "- Phải có ít nhất 8 ký tự";
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbPassword.Focus();
                    return;
                }
            }

            Regex regexPhoneNumber = new Regex(@"^0[1-9][0-9]{8}$");
            match = regexPhoneNumber.Match(phoneNumber);
            if (!match.Success)
            {
                MessageBox.Show("SDT sai định dạng: 0XXXXXXXXX", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbPhoneNumber.Focus();
                return;
            }

            if (String.IsNullOrEmpty(fullname))
            {
                MessageBox.Show("Thiếu thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbFullname.Focus();
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có muốn sửa thông tin tài khoản này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (AccountDAO.Instance.UpdateAccout(username, password, fullname, phoneNumber, email, roleID))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                    LoadAccounts();
                }
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            txbUsername.Text = "";
            txbPassword.Text = "";
            txbFullname.Text = "";
            txbPhoneNumber.Text = "";
            txbEmail.Text = "";

            cbRole.SelectedIndex = 0;

            btnUpdateAccount.Enabled = false;

            dtgvAccount.ClearSelection();
        }
        #endregion
    }
}
