namespace CafeShopManagementSystem
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnRevenue = new System.Windows.Forms.Button();
            this.btnWarehouse = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.lbFullname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.accountForm1 = new CafeShopManagementSystem.AccountForm();
            this.revenueReportForm1 = new CafeShopManagementSystem.RevenueReportForm();
            this.warehouseForm1 = new CafeShopManagementSystem.WarehouseForm();
            this.productForm1 = new CafeShopManagementSystem.ProductForm();
            this.orderForm1 = new CafeShopManagementSystem.OrderForm();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(97)))), ((int)(((byte)(59)))));
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbFullname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(245, 802);
            this.panel1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(6, 745);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(232, 46);
            this.button5.TabIndex = 8;
            this.button5.Text = "Đăng xuất";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.LogoutEvt);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAccount);
            this.panel2.Controls.Add(this.btnRevenue);
            this.panel2.Controls.Add(this.btnWarehouse);
            this.panel2.Controls.Add(this.btnProduct);
            this.panel2.Controls.Add(this.btnOrder);
            this.panel2.Location = new System.Drawing.Point(2, 192);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(239, 318);
            this.panel2.TabIndex = 4;
            // 
            // btnAccount
            // 
            this.btnAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.btnAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccount.ForeColor = System.Drawing.Color.White;
            this.btnAccount.Location = new System.Drawing.Point(2, 259);
            this.btnAccount.Margin = new System.Windows.Forms.Padding(2);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(232, 46);
            this.btnAccount.TabIndex = 11;
            this.btnAccount.Text = "Tài khoản";
            this.btnAccount.UseVisualStyleBackColor = false;
            this.btnAccount.Click += new System.EventHandler(this.OpenAccountFormEvt);
            // 
            // btnRevenue
            // 
            this.btnRevenue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.btnRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenue.ForeColor = System.Drawing.Color.White;
            this.btnRevenue.Location = new System.Drawing.Point(2, 195);
            this.btnRevenue.Margin = new System.Windows.Forms.Padding(2);
            this.btnRevenue.Name = "btnRevenue";
            this.btnRevenue.Size = new System.Drawing.Size(232, 46);
            this.btnRevenue.TabIndex = 10;
            this.btnRevenue.Text = "Báo cáo doanh thu";
            this.btnRevenue.UseVisualStyleBackColor = false;
            this.btnRevenue.Click += new System.EventHandler(this.OpenReportFormEvt);
            // 
            // btnWarehouse
            // 
            this.btnWarehouse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWarehouse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.btnWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWarehouse.ForeColor = System.Drawing.Color.White;
            this.btnWarehouse.Location = new System.Drawing.Point(2, 131);
            this.btnWarehouse.Margin = new System.Windows.Forms.Padding(2);
            this.btnWarehouse.Name = "btnWarehouse";
            this.btnWarehouse.Size = new System.Drawing.Size(232, 46);
            this.btnWarehouse.TabIndex = 9;
            this.btnWarehouse.Text = "Kho hàng";
            this.btnWarehouse.UseVisualStyleBackColor = false;
            this.btnWarehouse.Click += new System.EventHandler(this.OpenWarehouseFormEvt);
            // 
            // btnProduct
            // 
            this.btnProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.btnProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduct.ForeColor = System.Drawing.Color.White;
            this.btnProduct.Location = new System.Drawing.Point(2, 68);
            this.btnProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(232, 46);
            this.btnProduct.TabIndex = 8;
            this.btnProduct.Text = "Mặt hàng";
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new System.EventHandler(this.OpenProductFormEvt);
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(95)))), ((int)(((byte)(55)))));
            this.btnOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(2, 2);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(232, 46);
            this.btnOrder.TabIndex = 7;
            this.btnOrder.Text = "Đơn hàng";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.OpenOrderFormEvt);
            // 
            // lbFullname
            // 
            this.lbFullname.AutoSize = true;
            this.lbFullname.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFullname.ForeColor = System.Drawing.Color.White;
            this.lbFullname.Location = new System.Drawing.Point(96, 132);
            this.lbFullname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFullname.Name = "lbFullname";
            this.lbFullname.Size = new System.Drawing.Size(47, 22);
            this.lbFullname.TabIndex = 3;
            this.lbFullname.Text = "name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fullname:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CafeShopManagementSystem.Properties.Resources.cafe_shop_logo;
            this.pictureBox1.Location = new System.Drawing.Point(52, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(122, 118);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.accountForm1);
            this.panel3.Controls.Add(this.revenueReportForm1);
            this.panel3.Controls.Add(this.warehouseForm1);
            this.panel3.Controls.Add(this.productForm1);
            this.panel3.Controls.Add(this.orderForm1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(245, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1295, 802);
            this.panel3.TabIndex = 1;
            // 
            // accountForm1
            // 
            this.accountForm1.Location = new System.Drawing.Point(0, 0);
            this.accountForm1.Name = "accountForm1";
            this.accountForm1.Size = new System.Drawing.Size(1295, 802);
            this.accountForm1.TabIndex = 4;
            this.accountForm1.Visible = false;
            // 
            // revenueReportForm1
            // 
            this.revenueReportForm1.Location = new System.Drawing.Point(0, 0);
            this.revenueReportForm1.Name = "revenueReportForm1";
            this.revenueReportForm1.Size = new System.Drawing.Size(1295, 802);
            this.revenueReportForm1.TabIndex = 3;
            // 
            // warehouseForm1
            // 
            this.warehouseForm1.Location = new System.Drawing.Point(0, 0);
            this.warehouseForm1.Name = "warehouseForm1";
            this.warehouseForm1.Size = new System.Drawing.Size(1295, 802);
            this.warehouseForm1.TabIndex = 2;
            // 
            // productForm1
            // 
            this.productForm1.Location = new System.Drawing.Point(0, 0);
            this.productForm1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.productForm1.Name = "productForm1";
            this.productForm1.Size = new System.Drawing.Size(1295, 802);
            this.productForm1.TabIndex = 1;
            // 
            // orderForm1
            // 
            this.orderForm1.Location = new System.Drawing.Point(0, 0);
            this.orderForm1.Margin = new System.Windows.Forms.Padding(2);
            this.orderForm1.Name = "orderForm1";
            this.orderForm1.Size = new System.Drawing.Size(1295, 802);
            this.orderForm1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1540, 802);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cafe Shop Management Systemm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFullname;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnRevenue;
        private System.Windows.Forms.Button btnWarehouse;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel3;
        private OrderForm orderForm1;
        private ProductForm productForm1;
        private WarehouseForm warehouseForm1;
        private RevenueReportForm revenueReportForm1;
        private AccountForm accountForm1;
    }
}