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

namespace InventoryManagementSystem.Forms
{
    public partial class ModifyProductForm : Form
    {
        private Product currentProduct;
        private BindingList<Part> associatedParts = new BindingList<Part>();
        public ModifyProductForm()
        {
            InitializeComponent();
        }

        private void ModifyProductForm_Load(object sender, EventArgs e)
        {
            // Populate text fields with product data
            txtID.Text = currentProduct.ProductID.ToString();
            txtName.Text = currentProduct.Name;
            txtInventory.Text = currentProduct.InStock.ToString();
            txtPrice.Text = currentProduct.Price.ToString();
            txtMin.Text = currentProduct.Min.ToString();
            txtMax.Text = currentProduct.Max.ToString();

            // Copy associated parts
            associatedParts = new BindingList<Part>(currentProduct.AssociatedParts.ToList());

            // Bind DataGrids
            dgvAllCandidateParts.DataSource = Inventory.AllParts;
            dgvAssociatedParts.DataSource = associatedParts;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            dgvAllCandidateParts.ClearSelection();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isNumeric = int.TryParse(searchTerm, out int partID);
            bool found = false;

            foreach (DataGridViewRow row in dgvAllCandidateParts.Rows)
            {
                Part part = row.DataBoundItem as Part;
                if (part != null)
                {
                    if ((isNumeric && part.PartID == partID) ||
                        (!isNumeric && part.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        row.Selected = true;
                        dgvAllCandidateParts.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("No matching parts found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvAllCandidateParts.CurrentRow == null || dgvAllCandidateParts.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a part to add.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Part selectedPart = dgvAllCandidateParts.CurrentRow.DataBoundItem as Part;

            if (associatedParts.Contains(selectedPart))
            {
                MessageBox.Show("This part is already added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            associatedParts.Add(selectedPart);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAssociatedParts.CurrentRow == null || dgvAssociatedParts.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a part to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Part selectedPart = dgvAssociatedParts.CurrentRow.DataBoundItem as Part;

            var result = MessageBox.Show($"Are you sure you want to remove {selectedPart.Name}?",
                                         "Confirm Removal",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                associatedParts.Remove(selectedPart);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                !int.TryParse(txtInventory.Text, out int inventory) ||
                !decimal.TryParse(txtPrice.Text, out decimal price) ||
                !int.TryParse(txtMin.Text, out int min) ||
                !int.TryParse(txtMax.Text, out int max))
            {
                MessageBox.Show("Please enter valid values for all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (min > max)
            {
                MessageBox.Show("Min cannot be greater than Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (inventory < min || inventory > max)
            {
                MessageBox.Show("Inventory must be between Min and Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update product
            currentProduct.Name = txtName.Text;
            currentProduct.InStock = inventory;
            currentProduct.Price = price;
            currentProduct.Min = min;
            currentProduct.Max = max;

            currentProduct.AssociatedParts.Clear();
            foreach (Part part in associatedParts)
            {
                currentProduct.AddAssociatedPart(part);
            }

            // Update inventory
            Inventory.UpdateProduct(currentProduct.ProductID, currentProduct);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Any unsaved changes will be lost.",
                                         "Cancel Confirmation",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
