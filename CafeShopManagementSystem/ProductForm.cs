using CafeShopManagementSystem.DAO;
using CafeShopManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class ProductForm : UserControl
    {
        public ProductForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadCategory();
        }

        #region Methods
        private void LoadProducts()
        {
            dtgvProduct.DataSource = ProductDAO.Instance.GetListProduct();
            dtgvProduct.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgvProduct.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgvProduct.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dtgvProduct.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadCategory()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";
        }

        private void ChangeFormProduct(params bool[] data)
        {
            btnAddProduct.Enabled = data[0];
            btnDeleteProduct.Enabled = data[1];
            btnEditProduct.Enabled = data[2];
        }

        private void ClearData()
        {
            txbID.Text = string.Empty;
            txbName.Text = string.Empty;
            txbPrice.Text = string.Empty;
            txbDescription.Text = string.Empty;

            pbProductImage.Image = null;
            pbProductImage.ImageLocation = null;

            cbCategory.SelectedIndex = 0;

            dtgvProduct.ClearSelection();
        }
        #endregion

        #region Events
        private void ImportPictureEvt(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
                string imagePath = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pbProductImage.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txbName.Text.Trim();
            string description = txbDescription.Text.Trim();
            int categoryID = (cbCategory.SelectedItem as Category).ID;

            if (!Double.TryParse(txbPrice.Text.Trim(), out double price) || String.IsNullOrEmpty(name) || price <= 0 || price >= 1000000000000)
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn muốn thêm sản phẩm này không", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string path = "";
                if (pbProductImage.ImageLocation != null)
                {
                    int newID = ProductDAO.Instance.GetMaxID() + 1;
                    path = Path.Combine(@"./uploads/" + newID + ".png");
                    string directoryPath = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    File.Copy(pbProductImage.ImageLocation, path, true);
                }

                if (ProductDAO.Instance.InsertProduct(name, price, path, description, categoryID))
                {
                    MessageBox.Show("Thêm sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    ClearData();
                }
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txbID.Text.Trim());
            string path = ProductDAO.Instance.GetThumbnailByID(id);

            string name = txbName.Text.Trim();
            string description = txbDescription.Text.Trim();
            int categoryID = (cbCategory.SelectedItem as Category).ID;

            if (!Double.TryParse(txbPrice.Text.Trim(), out double price) || String.IsNullOrEmpty(name) || price <= 0 || price >= 1000000000000)
            {
                MessageBox.Show("Thông tin không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn muốn sửa sản phẩm này không", "Thanh toán", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (String.IsNullOrEmpty(path))
                {
                    if (pbProductImage.ImageLocation != null)
                    {
                        path = Path.Combine(@"./uploads/" + id + ".png");
                        string directoryPath = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        File.Copy(pbProductImage.ImageLocation, path, true);
                    }
                }
                else
                {
                    if (path != pbProductImage.ImageLocation.ToString())
                        File.Copy(pbProductImage.ImageLocation, path, true);
                }

                if (ProductDAO.Instance.UpdateProduct(id, name, price, path, description, categoryID))
                {
                    MessageBox.Show("Sửa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                }
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int id = Int32.Parse(txbID.Text.Trim());
                if (ProductDAO.Instance.DeleteProduct(id))
                {
                    if (pbProductImage.ImageLocation != null)
                    {
                        File.Delete(pbProductImage.ImageLocation.ToString());
                        pbProductImage.ImageLocation = null;
                        pbProductImage.Image = null;
                    }
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    LoadProducts();

                    ClearData();
                    ChangeFormProduct(true, false, false);
                }
            }
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            btnDeleteData.Text = "Xóa dữ liệu";
            ClearData();
            ChangeFormProduct(true, false, false);
        }

        private void dtgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvProduct.SelectedRows.Count > 0)
            {
                btnDeleteData.Text = "Hủy chọn";
                ChangeFormProduct(false, true, true);

                int id = (int)dtgvProduct.SelectedRows[0].Cells[0].Value;
                Product product = ProductDAO.Instance.GetProductByID(id);

                txbID.Text = product.Id.ToString();
                txbName.Text = product.Name;
                txbPrice.Text = product.Price.ToString();
                txbDescription.Text = product.Description;

                if (!String.IsNullOrEmpty(product.Thumbnail))
                {
                    pbProductImage.ImageLocation = product.Thumbnail;
                }
                else
                {
                    pbProductImage.ImageLocation = null;
                    pbProductImage.Image = null;
                }

                cbCategory.SelectedIndex = product.CategoryID - 1;

            }
        }
        #endregion
    }
}
