using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class MainForm : Form
    {
        private Account account;

        public Account Account
        {
            get { return account; }
            set
            {
                account = value;
                ChangeFormByRole(account.RoleID);
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        #region Methods
        private void OpenForm(bool order, bool products, bool warehouse, bool revenue, bool account)
        {
            orderForm1.Visible = order;
            productForm1.Visible = products;
            warehouseForm1.Visible = warehouse;
            revenueReportForm1.Visible = revenue;
            accountForm1.Visible = account;
        }

        private void ShowButtonByRole(bool order, bool products, bool warehouse, bool revenue)
        {
            btnOrder.Enabled = order;
            btnProduct.Enabled = products;
            btnWarehouse.Enabled = warehouse;
            btnRevenue.Enabled = revenue;
        }

        private void ChangeFormByRole(int role)
        {
            lbFullname.Text = account.Fullname;
            if (role == 1)
            {
                ShowButtonByRole(true, true, true, true);
            }
            else if (role == 2)
            {
                ShowButtonByRole(true, true, false, true);
            }
            else if (role == 3)
            {
                ShowButtonByRole(false, false, true, true);
            }

        }
        #endregion

        #region Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (account.RoleID == 3)
                OpenForm(false, false, true, false, false);
            else
                OpenForm(true, false, false, false, false);
        }

        private void OpenOrderFormEvt(object sender, EventArgs e)
        {
            OpenForm(true, false, false, false, false);
        }

        private void OpenProductFormEvt(object sender, EventArgs e)
        {
            OpenForm(false, true, false, false, false);
        }

        private void OpenWarehouseFormEvt(object sender, EventArgs e)
        {
            OpenForm(false, false, true, false, false);
        }

        private void OpenReportFormEvt(object sender, EventArgs e)
        {
            OpenForm(false, false, false, true, false);
            revenueReportForm1.LoadRevenue();
        }

        private void OpenAccountFormEvt(object sender, EventArgs e)
        {
            if (account.RoleID == 1)
            {
                OpenForm(false, false, false, false, true);
            }
            else
            {
                ProfileForm profileForm = new ProfileForm();
                profileForm.Account = (Account)account.Clone();

                profileForm.ShowDialog();
            }
        }

        private void LogoutEvt(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                account = null;
                Close();
            }
        }
        #endregion
    }
}
