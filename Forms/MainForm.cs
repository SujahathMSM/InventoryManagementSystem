using InventoryManagementSystem.Forms;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridViews(); 
            LoadSampleData(); 
        }

        private void SetupDataGridViews()
        {
            dgvParts.Columns.Add("PartID", "Part ID");
            dgvParts.Columns.Add("PartName", "Name");
            dgvParts.Columns.Add("Inventory", "Inventory");
            dgvParts.Columns.Add("Price", "Price");
            dgvParts.Columns.Add("Min", "Min");
            dgvParts.Columns.Add("Max", "Max");

            dgvProducts.Columns.Add("ProductID", "Product ID");
            dgvProducts.Columns.Add("ProductName", "Name");
            dgvProducts.Columns.Add("Inventory", "Inventory");
            dgvProducts.Columns.Add("Price", "Price");
            dgvProducts.Columns.Add("Min", "Min");
            dgvProducts.Columns.Add("Max", "Max");
        }

        private void LoadSampleData()
        {
            dgvParts.Rows.Add(0, "Wheel", 15, 12.11, 5, 25);
            dgvParts.Rows.Add(1, "Pedal", 8, 8.22, 5, 25);
            dgvParts.Rows.Add(2, "Chain", 8, 8.33, 5, 25);
            dgvParts.Rows.Add(3, "Seat", 8, 4.55, 2, 25);

            dgvProducts.Rows.Add(0, "Red Bicycle", 15, 11.44, 1, 25);
            dgvProducts.Rows.Add(1, "Yellow Bicycle", 19, 9.66, 1, 20);
            dgvProducts.Rows.Add(2, "Blue Bicycle", 5, 12.77, 1, 25);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Are you sure you want to exit?",
               "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        private void RefreshDataGridViews()
        {
            dgvParts.Rows.Clear();
            dgvProducts.Rows.Clear();

            foreach (var part in Inventory.AllParts)
            {
                dgvParts.Rows.Add(part.PartID, part.Name, part.InStock, part.Price, part.Min, part.Max);
            }

            foreach (var product in Inventory.Products)
            {
                dgvProducts.Rows.Add(product.ProductID, product.Name, product.InStock, product.Price, product.Min, product.Max);
            }
        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            AddPartForm addPartForm = new AddPartForm();
            if (addPartForm.ShowDialog() == DialogResult.OK)
            {
                RefreshDataGridViews();  // Refresh the UI with new data
            }
        }

        private void btnModifyPart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow != null)
            {
                int partID = (int)dgvParts.CurrentRow.Cells["PartID"].Value;
                Part selectedPart = Inventory.LookupPart(partID);

                if (selectedPart != null)
                {
                    ModifyPartForm modifyPartForm = new ModifyPartForm(selectedPart);
                    if (modifyPartForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshDataGridViews();
                    }
                }
                else
                {
                    MessageBox.Show("Error: Part not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a part to modify.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            if (addProductForm.ShowDialog() == DialogResult.OK) { RefreshDataGridViews(); }
        }
    }


}
