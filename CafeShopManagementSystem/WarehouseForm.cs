using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class WarehouseForm : UserControl
    {
        private int currentReceiptID = -1;
        private int currentInventoryID = -1;

        public WarehouseForm()
        {
            InitializeComponent();
            LoadComboBoxGood();
            // Good
            LoadGoods();
            // Receipt
            LoadReceipts();
            // Inventory
            LoadInventories();
        }

        #region Methods
        private void LoadComboBoxGood()
        {
            cbGoodRe.DataSource = GoodDAO.Instance.GetListGoodIsActive();
            cbGoodRe.DisplayMember = "goodName";

            cbGoodIn.DataSource = GoodDAO.Instance.GetListGoodIsActive();
            cbGoodIn.DisplayMember = "goodName";
        }
        #endregion

        #region Methods Good
        private void LoadGoods()
        {
            dtgvGood.DataSource = GoodDAO.Instance.GetListGood();
            dtgvGood.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ChangeFormGood(params bool[] data)
        {
            btnAddGood.Enabled = data[0];
            btnEditGood.Enabled = data[1];
            btnDeleteGood.Enabled = data[2];
        }

        private void ClearGoodData()
        {
            txbID.Text = string.Empty;
            txbName.Text = string.Empty;
            txbPrice.Text = string.Empty;

            rbNumberOf.Checked = false;
            rbAmountOf.Checked = false;

            dtgvGood.ClearSelection();
        }
        #endregion

        #region Methods Receipt
        private void LoadReceipts()
        {
            dtgvReceipt.DataSource = ReceiptDAO.Instance.GetListReceipt();
            dtgvReceipt.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgvReceipt.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadReceiptInfo(int id)
        {
            dtgvGoodInReceipt.DataSource = GoodDAO.Instance.GetGoodInReceipt(id);
            dtgvGoodInReceipt.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ChangeButtonByConfirm(params bool[] data)
        {
            // Enable
            btnAddReceipt.Enabled = data[0];
            btnEditReceipt.Enabled = data[1];
            btnDeleteReceipt.Enabled = data[2];

            // Visible
            btnConfirm.Visible = data[3];
            btnAddGoodRe.Visible = data[4];
            btnDeleteGoodRe.Visible = data[5];
        }

        private void ChangeTextboxByConfirm(params bool[] data)
        {
            txbRecipientName.ReadOnly = data[0];
            txbSupplierName.ReadOnly = data[1];
        }

        private void ClearReceiptData()
        {
            txbRecipientName.Text = string.Empty;
            txbSupplierName.Text = string.Empty;
            txbCountRe.Text = string.Empty;

            cbGoodRe.SelectedIndex = 0;

            dtgvGoodInReceipt.DataSource = null;
            dtgvGoodInReceipt.Rows.Clear();
            dtgvGoodInReceipt.ClearSelection();

            currentReceiptID = -1;
        }
        #endregion

        #region Methods Inventory
        private void LoadInventories()
        {
            dtgvInventory.DataSource = InventoryDAO.Instance.GetListInventory();
            dtgvInventory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadInventoryInfo(int id)
        {
            dtgvGoodInInventory.DataSource = GoodDAO.Instance.GetGoodInInventory(id);
            dtgvGoodInInventory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void ChangeFormInventory(params bool[] data)
        {
            btnAddIn.Enabled = data[0];

            btnAddGoodIn.Visible = data[1];
            btnDeleteGoodIn.Visible = data[2];

            txbWriterName.ReadOnly = data[3];
        }

        private void ClearInventoryData()
        {
            txbWriterName.Text = string.Empty;
            txbCountIn.Text = string.Empty;

            cbGoodIn.SelectedIndex = 0;

            dtgvGoodInInventory.DataSource = null;
            dtgvGoodInInventory.Rows.Clear();
            dtgvGoodInInventory.ClearSelection();
        }
        #endregion

        #region Events Good
        private void dtgvGood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvGood.SelectedRows.Count > 0)
            {
                ChangeFormGood(false, true, true);

                int id = (int)dtgvGood.SelectedCells[0].OwningRow.Cells["Mã"].Value;
                Good good = GoodDAO.Instance.GetGoodByID(id);

                txbID.Text = good.Id.ToString();
                txbName.Text = good.GoodName;
                txbPrice.Text = good.Price.ToString();

                if (good.Type == 1)
                    rbNumberOf.Checked = true;
                else
                    rbAmountOf.Checked = true;
            }
        }

        private void btnClearGood_Click(object sender, EventArgs e)
        {
            ClearGoodData();
            ChangeFormGood(true, false, false);
        }

        private void btnAddGood_Click(object sender, EventArgs e)
        {
            string name = txbName.Text.Trim();
            double price = 0;
            int type = -1;

            if (rbNumberOf.Checked) { type = 1; }
            else if (rbAmountOf.Checked) { type = 0; }

            if (!Double.TryParse(txbPrice.Text.Trim(), out price) || String.IsNullOrEmpty(name) || type == -1 || price <= 0)
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn muốn thêm hàng hóa này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (GoodDAO.Instance.InsertGood(name, price, type))
                {
                    MessageBox.Show("Thêm hàng hóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGoods();
                    LoadComboBoxGood();
                    ClearGoodData();
                }
            }
        }

        private void btnEditGood_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txbID.Text.Trim());
            string name = txbName.Text.Trim();
            double price = 0;
            int type = -1;

            if (rbNumberOf.Checked) { type = 1; }
            else if (rbAmountOf.Checked) { type = 0; }

            if (!Double.TryParse(txbPrice.Text.Trim(), out price) || String.IsNullOrEmpty(name) || type == -1 || price <= 0)
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn muốn sửa hàng hóa này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (GoodDAO.Instance.UpdateGood(id, name, price, type))
                {
                    MessageBox.Show("Sửa hàng hóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGoods();
                    LoadComboBoxGood();
                    ClearGoodData();
                }
            }
        }

        private void btnDeleteGood_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa hàng hóa này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(txbID.Text.Trim());
                if (GoodDAO.Instance.DeleteGood(id))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    LoadGoods();
                    LoadComboBoxGood();
                    ClearGoodData();
                    ChangeFormGood(true, false, false);
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string search = txbSearchGood.Text.Trim();
            if (string.IsNullOrEmpty(search))
            {
                LoadGoods();
            }
            else
            {
                dtgvGood.DataSource = GoodDAO.Instance.SearchGoodByName(search);
            }
        }
        #endregion

        #region Events Receipt
        private void cbGoodRe_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Good good = cb.SelectedItem as Good;
            int type = good.Type;
            txbCountRe.Text = string.Empty;

            if (type == 1)
                lbUnitRe.Text = "Cái";
            else
                lbUnitRe.Text = "Kg";
        }

        private void btnAddGoodRe_Click(object sender, EventArgs e)
        {
            if (cbGoodRe.SelectedItem != null)
            {
                Good good = cbGoodRe.SelectedItem as Good;
                int id = good.Id;
                int type = good.Type;

                double countFloat = 0;
                int countInt = 0;
                if ((!Double.TryParse(txbCountRe.Text.Trim(), out countFloat) && type == 0) || countFloat <= 0)
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if ((!Int32.TryParse(txbCountRe.Text.Trim(), out countInt) && type == 1) || countInt <= 0)
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (currentReceiptID == -1)
                {
                    ReceiptDAO.Instance.InsertReceipt();
                    currentReceiptID = ReceiptDAO.Instance.GetMaxID();
                    if (type == 0)
                        ReceiptDAO.Instance.InsertReceiptInfo(currentReceiptID, id, countFloat);
                    else if (type == 1)
                        ReceiptDAO.Instance.InsertReceiptInfo(currentReceiptID, id, (double)countInt);
                }
                else
                {
                    if (type == 0)
                        ReceiptDAO.Instance.InsertReceiptInfo(currentReceiptID, id, countFloat);
                    else if (type == 1)
                        ReceiptDAO.Instance.InsertReceiptInfo(currentReceiptID, id, (float)countInt);
                }

                LoadReceiptInfo(currentReceiptID);
                LoadReceipts();
            }
        }

        private void btnDeleteGoodRe_Click(object sender, EventArgs e)
        {
            if (dtgvGoodInReceipt.SelectedRows.Count > 0)
            {
                int index = dtgvGoodInReceipt.SelectedRows[0].Index;
                int idGood = (int)dtgvGoodInReceipt.SelectedRows[0].Cells[0].Value;
                double cnt = (double)dtgvGoodInReceipt.SelectedRows[0].Cells[2].Value;
                ReceiptDAO.Instance.ReduceGoodInReceipt(currentReceiptID, idGood);
                LoadReceiptInfo(currentReceiptID);
                if (cnt > 1) dtgvGoodInReceipt.Rows[index].Selected = true;
            }
        }

        private void btnAddReceipt_Click(object sender, EventArgs e)
        {
            if (currentReceiptID == -1)
            {
                MessageBox.Show("Chưa có phiếu nhập hàng!");
                return;
            }

            string recipitentName = txbRecipientName.Text.Trim();
            string supplierName = txbSupplierName.Text.Trim();

            if ((String.IsNullOrEmpty(recipitentName) || String.IsNullOrEmpty(supplierName)))
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int countInReceipt = ReceiptDAO.Instance.GetCountInReceipt(currentReceiptID);
            if (countInReceipt <= 0)
            {
                MessageBox.Show("Phiếu chưa có hàng hóa nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ReceiptDAO.Instance.UpdateReceiptAfterAdd(currentReceiptID, recipitentName, supplierName))
            {
                LoadReceipts();
                ClearReceiptData();
            }
        }

        private void btnEditReceipt_Click(object sender, EventArgs e)
        {
            if (dtgvReceipt.SelectedRows.Count > 0)
            {
                string recipitentName = txbRecipientName.Text.Trim();
                string supplierName = txbSupplierName.Text.Trim();

                if ((String.IsNullOrEmpty(recipitentName) || String.IsNullOrEmpty(supplierName)))
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ReceiptDAO.Instance.UpdateReceipt(currentReceiptID, recipitentName, supplierName))
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK);
                    LoadReceipts();
                }
            }
        }

        private void btnDeleteReceipt_Click(object sender, EventArgs e)
        {
            if (dtgvReceipt.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa phiếu này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (ReceiptDAO.Instance.DeleteReceipt(currentReceiptID))
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                        LoadReceipts();

                        ClearReceiptData();
                        ChangeButtonByConfirm(true, false, false, false, true, true);
                        ChangeTextboxByConfirm(false, false);
                    }
                }
            }
        }

        private void dtgvReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvReceipt.SelectedRows.Count > 0)
            {
                int id = (int)dtgvReceipt.SelectedRows[0].Cells[0].Value;
                Receipt receipt = ReceiptDAO.Instance.GetReceiptByID(id);
                currentReceiptID = receipt.Id;
                int comfirm = receipt.Confirm;

                txbRecipientName.Text = receipt.RecipitentName;
                txbSupplierName.Text = receipt.SupplierName;

                if (comfirm == 0)
                {
                    ChangeButtonByConfirm(true, false, false, false, true, true);
                    ChangeTextboxByConfirm(false, false);
                }
                else if (comfirm == 1)
                {
                    ChangeButtonByConfirm(false, true, true, true, false, false);
                    ChangeTextboxByConfirm(false, false);
                }
                else
                {
                    ChangeButtonByConfirm(false, false, false, false, false, false);
                    ChangeTextboxByConfirm(true, true);
                }

                LoadReceiptInfo(currentReceiptID);
            }
        }

        private void btnClearReceipt_Click(object sender, EventArgs e)
        {
            ClearReceiptData();
            ChangeButtonByConfirm(true, false, false, false, true, true);
            ChangeTextboxByConfirm(false, false);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dtgvReceipt.SelectedRows.Count > 0)
            {
                string mess = "Bạn có muốn thực hiện xác nhận phiếu nhập kho này này không?\nHãy chắc chắn bạn đã kiểm tra kỹ trước khi thực hiện.";
                DialogResult result = MessageBox.Show(mess, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ReceiptDAO.Instance.ConfirmReceipt(currentReceiptID);

                    LoadReceipts();
                    ChangeButtonByConfirm(false, false, false, false, false, false);
                    ChangeTextboxByConfirm(true, true);
                }
            }
        }

        private void btnFilterReceipt_Click(object sender, EventArgs e)
        {
            string dateFrom = dtpkFromDateRe.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpkToDateRe.Value.ToString("yyyy-MM-dd");
            dtgvReceipt.DataSource = ReceiptDAO.Instance.GetReceiptByDate(dateFrom, dateTo);
        }
        #endregion

        #region Events Inventory
        private void cbGoodIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Good good = cb.SelectedItem as Good;
            int type = good.Type;
            txbCountIn.Text = string.Empty;

            if (type == 1)
                lbUnitIn.Text = "Cái";
            else
                lbUnitIn.Text = "Kg";
        }


        private void btnAddGoodIn_Click(object sender, EventArgs e)
        {
            if (cbGoodIn.SelectedItem != null)
            {
                Good good = cbGoodIn.SelectedItem as Good;
                int id = good.Id;
                int type = good.Type;

                double countFloat = 0;
                int countInt = 0;
                if ((!Double.TryParse(txbCountIn.Text.Trim(), out countFloat) && type == 0) || countFloat <= 0)
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if ((!Int32.TryParse(txbCountIn.Text.Trim(), out countInt) && type == 1) || countInt <= 0)
                {
                    MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (currentInventoryID == -1)
                {
                    InventoryDAO.Instance.InsertInventory();
                    currentInventoryID = InventoryDAO.Instance.GetMaxID();
                    if (type == 0)
                        InventoryDAO.Instance.InsertInventoryInfo(currentInventoryID, id, countFloat);
                    else if (type == 1)
                        InventoryDAO.Instance.InsertInventoryInfo(currentInventoryID, id, (double)countInt);
                }
                else
                {
                    if (type == 0)
                        InventoryDAO.Instance.InsertInventoryInfo(currentInventoryID, id, countFloat);
                    else if (type == 1)
                        InventoryDAO.Instance.InsertInventoryInfo(currentInventoryID, id, (double)countInt);
                }

                LoadInventoryInfo(currentInventoryID);
                LoadInventories();
            }
        }

        private void dtgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvInventory.SelectedRows.Count > 0)
            {
                int id = (int)dtgvInventory.SelectedRows[0].Cells[0].Value;
                Inventory inventory = InventoryDAO.Instance.GetInventoryByID(id);
                currentInventoryID = inventory.Id;

                txbWriterName.Text = inventory.WriterName;

                if (String.IsNullOrEmpty(inventory.WriterName))
                {
                    ChangeFormInventory(true, true, true, false);
                }
                else
                {
                    ChangeFormInventory(false, false, false, true);
                }

                LoadInventoryInfo(id);
            }
        }

        private void btnDeleteGoodIn_Click(object sender, EventArgs e)
        {
            if (dtgvGoodInInventory.SelectedRows.Count > 0)
            {
                int index = dtgvGoodInInventory.SelectedRows[0].Index;
                int idGood = (int)dtgvGoodInInventory.SelectedRows[0].Cells[0].Value;
                double cnt = (double)dtgvGoodInInventory.SelectedRows[0].Cells[2].Value;
                InventoryDAO.Instance.ReduceGoodInInventory(currentInventoryID, idGood);
                LoadInventoryInfo(currentInventoryID);
                if (cnt > 1) dtgvGoodInInventory.Rows[index].Selected = true;
            }
        }

        private void btnAddIn_Click(object sender, EventArgs e)
        {
            if (currentInventoryID == -1)
            {
                MessageBox.Show("Chưa có báo cáo tồn kho!");
                return;
            }

            string writerName = txbWriterName.Text.Trim();

            if (String.IsNullOrEmpty(writerName))
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int countInInventory = InventoryDAO.Instance.GetCountInInventory(currentInventoryID);
            if (countInInventory <= 0)
            {
                MessageBox.Show("Báo cáo chưa có hàng hóa nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InventoryDAO.Instance.UpdateInventoryAfterAdd(currentInventoryID, writerName))
            {
                LoadInventories();
                ClearInventoryData();
            }
        }

        private void btnClearIn_Click(object sender, EventArgs e)
        {
            ClearInventoryData();
            ChangeFormInventory(true, true, true, false);
        }

        private void btnFilterInventory_Click(object sender, EventArgs e)
        {
            string dateFrom = dtpkFromDateIn.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpkToDateIn.Value.ToString("yyyy-MM-dd");
            dtgvInventory.DataSource = InventoryDAO.Instance.GetInventoryByDate(dateFrom, dateTo);
        }
        #endregion
    }
}
