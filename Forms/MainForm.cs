﻿using InventoryManagementSystem.Forms;
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
                RefreshDataGridViews();  
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

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected in the DataGridView
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to modify.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieve the ProductID from the selected row's cell
            int productID = (int)dgvProducts.CurrentRow.Cells["ProductID"].Value;

            // Look up the selected product using the ProductID
            Product selectedProduct = Inventory.LookupProduct(productID);

            if (selectedProduct != null)
            {
                // Create and display the ModifyProductForm
                ModifyProductForm form = new ModifyProductForm(selectedProduct);

                // Show the form and check the result
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the DataGridViews after modification
                    RefreshDataGridViews();
                }
            }
            else
            {
                // If product not found, show error message
                MessageBox.Show("Error: Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null)
            {
                MessageBox.Show("Please select a part to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int partID = (int)dgvParts.CurrentRow.Cells["PartID"].Value;
            Part selectedPart = Inventory.LookupPart(partID);

            if (selectedPart == null)
            {
                MessageBox.Show("Error: Part not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var product in Inventory.Products)
            {
                if (product.AssociatedParts.Contains(selectedPart))
                {
                    MessageBox.Show("This part is associated with a product and cannot be deleted.",
                                    "Delete Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
            }

            // Confirm deletion
            var result = MessageBox.Show($"Are you sure you want to delete {selectedPart.Name}?",
                                         "Confirm Deletion",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Inventory.DeletePart(selectedPart);
                RefreshDataGridViews(); 
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productID = (int)dgvProducts.CurrentRow.Cells["ProductID"].Value;
            Product selectedProduct = Inventory.LookupProduct(productID);

            if (selectedProduct == null)
            {
                MessageBox.Show("Error: Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            var result = MessageBox.Show($"Are you sure you want to delete {selectedProduct.Name}?",
                                         "Confirm Deletion",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Inventory.RemoveProduct(selectedProduct.ProductID);
                RefreshDataGridViews(); 
            }
        }

        private void btnSearchParts_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchParts.Text.Trim();

            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a search query.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear any previous selection
            dgvParts.ClearSelection();
            bool found = false;

            // Check if the query is numeric, and search by PartID if so.
            if (int.TryParse(searchQuery, out int partID))
            {
                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    if (row.Cells["PartID"].Value != null &&
                        int.TryParse(row.Cells["PartID"].Value.ToString(), out int currentID) &&
                        currentID == partID)
                    {
                        row.Selected = true;
                        // Optionally scroll to the row
                        dgvParts.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        // Uncomment the next line if you want to stop after the first match.
                        // break;
                    }
                }
            }
            else // Search by PartName (case-insensitive)
            {
                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    if (row.Cells["PartName"].Value != null &&
                        row.Cells["PartName"].Value.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        row.Selected = true;
                        dgvParts.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        // Uncomment the next line if you want to stop after the first match.
                        // break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("No matching parts found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnSearchProducts_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearchProducts.Text.Trim();

            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Please enter a search query.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Clear any previous selection
            dgvProducts.ClearSelection();
            bool found = false;

            // Check if the query is numeric, and search by ProductID if so.
            if (int.TryParse(searchQuery, out int productID))
            {
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductID"].Value != null &&
                        int.TryParse(row.Cells["ProductID"].Value.ToString(), out int currentID) &&
                        currentID == productID)
                    {
                        row.Selected = true;
                        dgvProducts.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        // Uncomment if you want to stop after the first match.
                        // break;
                    }
                }
            }
            else // Search by ProductName (case-insensitive)
            {
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["ProductName"].Value != null &&
                        row.Cells["ProductName"].Value.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        row.Selected = true;
                        dgvProducts.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        // Uncomment if you want to stop after the first match.
                        // break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("No matching products found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }


}
