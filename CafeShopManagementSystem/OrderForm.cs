using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CafeShopManagementSystem
{
    public partial class OrderForm : UserControl
    {
        private int currentID = -1;

        public OrderForm()
        {
            InitializeComponent();
            LoadOrders();
            LoadCategory();
            LoadStatus();
            LoadShippingMethod();
            LoadPaymentMethod();
        }

        #region Methods
        private void LoadCategory()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";
        }

        private void LoadStatus()
        {
            List<Status> statuses = StatusDAO.Instance.GetListStatus();
            cbStatus.DataSource = statuses;
            cbStatus.DisplayMember = "name";
        }

        private void LoadShippingMethod()
        {
            List<ShippingMethod> shippingMethods = ShippingMethodDAO.Instance.GetListShippingMethod();
            cbShippingMethod.DataSource = shippingMethods;
            cbShippingMethod.DisplayMember = "name";
        }

        private void LoadPaymentMethod()
        {
            List<PaymentMethod> paymentMethods = PaymentMethodDAO.Instance.GetListPaymentMethod();
            cbPaymentMethod.DataSource = paymentMethods;
            cbPaymentMethod.DisplayMember = "name";
        }

        private void LoadProductByCategoryID(int id)
        {
            dtgvProduct.DataSource = ProductDAO.Instance.GetProductsByCategoryID(id);
            dtgvProduct.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadProductInOrder(int id)
        {
            dtgvProductInOrder.DataSource = ProductDAO.Instance.GetProductInOrder(id);
            dtgvProductInOrder.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            double totalMoney = OrderDAO.Instance.CalculateTotalMoney(id);
            CultureInfo info = CultureInfo.GetCultureInfo("vi-VN");
            lbTotalMoney.Text = String.Format(info, "{0:c}", totalMoney);
        }

        private void LoadOrders()
        {
            dtgvOrder.DataSource = OrderDAO.Instance.GetListOrder();
            dtgvOrder.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ChangeButtonByOrderType(params bool[] data)
        {
            btnPayment.Visible = data[0];
            btnUpdate.Visible = data[1];
            btnDelete.Visible = data[2];
            btnCancel.Visible = data[3];
            btnAddProduct.Visible = data[4];
            btnRemoveProduct.Visible = data[5];
        }

        private void ChangeTextboxByOrderType(params bool[] data)
        {
            txbNote.ReadOnly = data[0];
            txbBuyerName.ReadOnly = data[1];
            txbAddress.ReadOnly = data[2];
            txbDiscount.ReadOnly = data[3];
            txbMoneyReceived.ReadOnly = data[4];
        }

        private void ChangeComboBoxByOrderType(params bool[] data)
        {
            cbShippingMethod.Enabled = data[0];
            cbPaymentMethod.Enabled = data[1];
            cbStatus.Enabled = data[2];
        }

        private void ClearData()
        {
            cbStatus.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            cbShippingMethod.SelectedIndex = 0;
            cbPaymentMethod.SelectedIndex = 0;

            txbNote.Text = string.Empty;
            txbBuyerName.Text = string.Empty;
            txbAddress.Text = string.Empty;
            txbDiscount.Text = "0";
            txbMoneyReceived.Text = string.Empty;

            lbTotalMoney.Text = "0";

            nbCount.Value = 1;

            dtgvProductInOrder.DataSource = null;
            dtgvProductInOrder.Rows.Clear();
            dtgvOrder.ClearSelection();

            currentID = -1;
        }
        #endregion

        #region Events
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null) return;

            Category selected = cb.SelectedItem as Category;
            int id = selected.ID;

            LoadProductByCategoryID(id);
        }

        private void cbShippingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null) return;

            int id = (cb.SelectedItem as ShippingMethod).ID;
            if (id == 3)
            {
                lbBuyerName.Visible = lbAddress.Visible = true;
                txbBuyerName.Visible = txbAddress.Visible = true;
            }
            else
            {
                lbBuyerName.Visible = lbAddress.Visible = false;
                txbBuyerName.Visible = txbAddress.Visible = false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (dtgvProduct.SelectedRows.Count > 0)
            {
                int id = (int)dtgvProduct.SelectedRows[0].Cells[0].Value;
                int count = (int)nbCount.Value;
                if (currentID == -1)
                {
                    OrderDAO.Instance.InsertOrder();
                    currentID = OrderDAO.Instance.GetMaxID();
                    OrderDAO.Instance.InsertOrderInfo(currentID, id, count);
                }
                else
                {
                    OrderDAO.Instance.InsertOrderInfo(currentID, id, count);
                }

                LoadProductInOrder(currentID);
                LoadOrders();
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dtgvProductInOrder.SelectedRows.Count > 0)
            {
                int index = dtgvProductInOrder.SelectedRows[0].Index;
                int idProduct = (int)dtgvProductInOrder.SelectedRows[0].Cells[0].Value;
                int cnt = (int)dtgvProductInOrder.SelectedRows[0].Cells[2].Value;
                OrderDAO.Instance.ReduceProductInOrder(currentID, idProduct);
                LoadProductInOrder(currentID);
                if (cnt > 1) dtgvProductInOrder.Rows[index].Selected = true;
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (currentID == -1)
            {
                MessageBox.Show("Chưa có đơn hàng!");
                return;
            }

            string note = txbNote.Text.Trim();
            string buyerName = txbBuyerName.Text.Trim();
            string address = txbAddress.Text.Trim();
            double totalMoney = OrderDAO.Instance.CalculateTotalMoney(currentID);
            double moneyReceived = 0;
            double discount = 0;
            int smID = (cbShippingMethod.SelectedItem as ShippingMethod).ID;
            int pmID = (cbPaymentMethod.SelectedItem as PaymentMethod).ID;

            if (!Double.TryParse(txbMoneyReceived.Text.Trim(), out moneyReceived) || !Double.TryParse(txbDiscount.Text.Trim(), out discount))
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (smID == 3 && (String.IsNullOrEmpty(buyerName) || String.IsNullOrEmpty(address)))
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double finalCost = totalMoney - (totalMoney * discount / 100);
            double change = moneyReceived - finalCost;
            if (change < 0)
            {
                MessageBox.Show("Khách hàng trả không đủ tiền!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (totalMoney <= 0)
            {
                MessageBox.Show("Đơn hàng chưa có sản phẩm nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CultureInfo info = CultureInfo.GetCultureInfo("vi-VN");
            string mess = $"Thanh toán đơn hàng\rTổng tiền: {String.Format(info, "{0:c}", finalCost)}\rTiền nhận: {String.Format(info, "{0:c}", moneyReceived)}\rTiền thừa: {String.Format(info, "{0:c}", change)}";
            DialogResult result = MessageBox.Show(mess, "Thanh toán", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                OrderDAO.Instance.UpdateOrderAfterPayment(note, buyerName, address, discount, totalMoney, smID, pmID, moneyReceived, currentID);
                LoadOrders();
                ClearData();
            }
        }

        private void dtgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvOrder.SelectedRows.Count > 0)
            {
                int id = (int)dtgvOrder.SelectedRows[0].Cells[0].Value;
                Order order = OrderDAO.Instance.GetOrderByID(id);
                currentID = order.Id;

                int type = order.StatusID;
                if (type == 1)
                {
                    ChangeButtonByOrderType(true, false, false, false, true, true);
                    ChangeTextboxByOrderType(false, false, false, false, false);
                    ChangeComboBoxByOrderType(true, true, false);
                }
                else if (type == 2 || type == 3)
                {
                    ChangeButtonByOrderType(false, true, true, true, false, false);
                    ChangeTextboxByOrderType(false, false, false, true, true);
                    ChangeComboBoxByOrderType(true, false, true);
                }
                else if (type == 4)
                {
                    ChangeButtonByOrderType(false, true, false, true, false, false);
                    ChangeTextboxByOrderType(true, true, true, true, true);
                    ChangeComboBoxByOrderType(false, false, true);
                }
                else
                {
                    ChangeButtonByOrderType(false, false, false, true, false, false);
                    ChangeTextboxByOrderType(true, true, true, true, true);
                    ChangeComboBoxByOrderType(false, false, false);
                }

                LoadProductInOrder(currentID);

                txbNote.Text = order.Note;
                txbDiscount.Text = order.Discount.ToString();
                txbMoneyReceived.Text = order.MoneyReceived.ToString();
                cbStatus.SelectedIndex = order.StatusID - 1;
                cbPaymentMethod.SelectedIndex = order.PaymentMethodID - 1;
                cbShippingMethod.SelectedIndex = order.ShippingMethodID - 1;
                if (order.ShippingMethodID == 3)
                {
                    txbBuyerName.Text = order.BuyerName;
                    txbAddress.Text = order.Address;
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string note = txbNote.Text.Trim();
            string buyerName = txbBuyerName.Text.Trim();
            string address = txbAddress.Text.Trim();
            int smID = (cbShippingMethod.SelectedItem as ShippingMethod).ID;
            int statusID = (cbStatus.SelectedItem as Status).ID;

            if (smID == 3 && (String.IsNullOrEmpty(buyerName) || String.IsNullOrEmpty(address)))
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (statusID == 1)
            {
                MessageBox.Show("Đơn hàng đã thanh toán bạn không thể đưa về trạng thái gọi đồ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (OrderDAO.Instance.UpdateOrder(note, buyerName, address, smID, statusID, currentID))
            {
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                LoadOrders();

                if (statusID == 2 || statusID == 3)
                {
                    ChangeButtonByOrderType(false, true, true, true, false, false);
                    ChangeTextboxByOrderType(false, false, false, true, true);
                    ChangeComboBoxByOrderType(true, false, true);
                }
                else if (statusID == 4)
                {
                    ChangeButtonByOrderType(false, true, false, true, false, false);
                    ChangeTextboxByOrderType(true, true, true, true, true);
                    ChangeComboBoxByOrderType(false, false, true);
                }
                else
                {
                    ChangeButtonByOrderType(false, false, false, true, false, false);
                    ChangeTextboxByOrderType(true, true, true, true, true);
                    ChangeComboBoxByOrderType(false, false, false);
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa đơn hàng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (OrderDAO.Instance.DeleteOrder(currentID))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    LoadOrders();

                    ClearData();
                    ChangeButtonByOrderType(true, false, false, false, true, true);
                    ChangeTextboxByOrderType(false, false, false, false, false);
                    ChangeComboBoxByOrderType(true, true, false);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
            ChangeButtonByOrderType(true, false, false, false, true, true);
            ChangeTextboxByOrderType(false, false, false, false, false);
            ChangeComboBoxByOrderType(true, true, false);
        }
        #endregion
    }
}
