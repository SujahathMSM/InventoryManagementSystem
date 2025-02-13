namespace InventoryManagementSystem
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvParts = new System.Windows.Forms.DataGridView();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblParts = new System.Windows.Forms.Label();
            this.lblProducts = new System.Windows.Forms.Label();
            this.txtSearchParts = new System.Windows.Forms.TextBox();
            this.txtSearchProducts = new System.Windows.Forms.TextBox();
            this.btnSearchParts = new System.Windows.Forms.Button();
            this.btnSearchProducts = new System.Windows.Forms.Button();
            this.btnAddPart = new System.Windows.Forms.Button();
            this.btnModifyPart = new System.Windows.Forms.Button();
            this.btnDeletePart = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnModifyProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(348, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Inventory Management System";
            // 
            // dgvParts
            // 
            this.dgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParts.Location = new System.Drawing.Point(72, 163);
            this.dgvParts.Name = "dgvParts";
            this.dgvParts.RowHeadersWidth = 51;
            this.dgvParts.RowTemplate.Height = 24;
            this.dgvParts.Size = new System.Drawing.Size(404, 150);
            this.dgvParts.TabIndex = 1;
            // 
            // dgvProducts
            // 
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(542, 163);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.RowTemplate.Height = 24;
            this.dgvProducts.Size = new System.Drawing.Size(409, 150);
            this.dgvProducts.TabIndex = 2;
            // 
            // lblParts
            // 
            this.lblParts.AutoSize = true;
            this.lblParts.Location = new System.Drawing.Point(69, 120);
            this.lblParts.Name = "lblParts";
            this.lblParts.Size = new System.Drawing.Size(38, 16);
            this.lblParts.TabIndex = 3;
            this.lblParts.Text = "Parts";
            // 
            // lblProducts
            // 
            this.lblProducts.AutoSize = true;
            this.lblProducts.Location = new System.Drawing.Point(539, 120);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(60, 16);
            this.lblProducts.TabIndex = 4;
            this.lblProducts.Text = "Products";
            // 
            // txtSearchParts
            // 
            this.txtSearchParts.Location = new System.Drawing.Point(359, 113);
            this.txtSearchParts.Name = "txtSearchParts";
            this.txtSearchParts.Size = new System.Drawing.Size(100, 22);
            this.txtSearchParts.TabIndex = 5;
            // 
            // txtSearchProducts
            // 
            this.txtSearchProducts.Location = new System.Drawing.Point(851, 120);
            this.txtSearchProducts.Name = "txtSearchProducts";
            this.txtSearchProducts.Size = new System.Drawing.Size(100, 22);
            this.txtSearchProducts.TabIndex = 6;
            // 
            // btnSearchParts
            // 
            this.btnSearchParts.Location = new System.Drawing.Point(263, 113);
            this.btnSearchParts.Name = "btnSearchParts";
            this.btnSearchParts.Size = new System.Drawing.Size(75, 23);
            this.btnSearchParts.TabIndex = 7;
            this.btnSearchParts.Text = "Search";
            this.btnSearchParts.UseVisualStyleBackColor = true;
            // 
            // btnSearchProducts
            // 
            this.btnSearchProducts.Location = new System.Drawing.Point(758, 119);
            this.btnSearchProducts.Name = "btnSearchProducts";
            this.btnSearchProducts.Size = new System.Drawing.Size(75, 23);
            this.btnSearchProducts.TabIndex = 8;
            this.btnSearchProducts.Text = "Search";
            this.btnSearchProducts.UseVisualStyleBackColor = true;
            // 
            // btnAddPart
            // 
            this.btnAddPart.Location = new System.Drawing.Point(276, 355);
            this.btnAddPart.Name = "btnAddPart";
            this.btnAddPart.Size = new System.Drawing.Size(62, 23);
            this.btnAddPart.TabIndex = 9;
            this.btnAddPart.Text = "Add";
            this.btnAddPart.UseVisualStyleBackColor = true;
            this.btnAddPart.Click += new System.EventHandler(this.btnAddPart_Click);
            // 
            // btnModifyPart
            // 
            this.btnModifyPart.Location = new System.Drawing.Point(344, 355);
            this.btnModifyPart.Name = "btnModifyPart";
            this.btnModifyPart.Size = new System.Drawing.Size(66, 23);
            this.btnModifyPart.TabIndex = 10;
            this.btnModifyPart.Text = "Modify";
            this.btnModifyPart.UseVisualStyleBackColor = true;
            this.btnModifyPart.Click += new System.EventHandler(this.btnModifyPart_Click);
            // 
            // btnDeletePart
            // 
            this.btnDeletePart.Location = new System.Drawing.Point(417, 355);
            this.btnDeletePart.Name = "btnDeletePart";
            this.btnDeletePart.Size = new System.Drawing.Size(59, 23);
            this.btnDeletePart.TabIndex = 11;
            this.btnDeletePart.Text = "Delete";
            this.btnDeletePart.UseVisualStyleBackColor = true;
            this.btnDeletePart.Click += new System.EventHandler(this.btnDeletePart_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(772, 355);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(53, 23);
            this.btnAddProduct.TabIndex = 12;
            this.btnAddProduct.Text = "Add";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnModifyProduct
            // 
            this.btnModifyProduct.Location = new System.Drawing.Point(831, 355);
            this.btnModifyProduct.Name = "btnModifyProduct";
            this.btnModifyProduct.Size = new System.Drawing.Size(56, 23);
            this.btnModifyProduct.TabIndex = 13;
            this.btnModifyProduct.Text = "Modify";
            this.btnModifyProduct.UseVisualStyleBackColor = true;
            this.btnModifyProduct.Click += new System.EventHandler(this.btnModifyProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Location = new System.Drawing.Point(893, 355);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(58, 23);
            this.btnDeleteProduct.TabIndex = 14;
            this.btnDeleteProduct.Text = "Delete";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(874, 445);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 37);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 503);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDeleteProduct);
            this.Controls.Add(this.btnModifyProduct);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnDeletePart);
            this.Controls.Add(this.btnModifyPart);
            this.Controls.Add(this.btnAddPart);
            this.Controls.Add(this.btnSearchProducts);
            this.Controls.Add(this.btnSearchParts);
            this.Controls.Add(this.txtSearchProducts);
            this.Controls.Add(this.txtSearchParts);
            this.Controls.Add(this.lblProducts);
            this.Controls.Add(this.lblParts);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.dgvParts);
            this.Controls.Add(this.lblTitle);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Screen";
            ((System.ComponentModel.ISupportInitialize)(this.dgvParts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvParts;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblParts;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.TextBox txtSearchParts;
        private System.Windows.Forms.TextBox txtSearchProducts;
        private System.Windows.Forms.Button btnSearchParts;
        private System.Windows.Forms.Button btnSearchProducts;
        private System.Windows.Forms.Button btnAddPart;
        private System.Windows.Forms.Button btnModifyPart;
        private System.Windows.Forms.Button btnDeletePart;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnModifyProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnExit;
    }
}

