using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Forms
{
    public partial class ModifyProductForm : Form
    {
        private Product currentProduct;
        private BindingList<Part> associatedParts = new BindingList<Part>();
        private ErrorProvider errorProvider = new ErrorProvider();

        public ModifyProductForm(Product productToModify)
        {
            InitializeComponent();
            this.currentProduct = productToModify;
            this.Load += ModifyProductForm_Load;
        }

        private void ModifyProductForm_Load(object sender, EventArgs e)
        {

            txtID.Text = currentProduct.ProductID.ToString();
            txtName.Text = currentProduct.Name;
            txtInventory.Text = currentProduct.InStock.ToString();
            txtPrice.Text = currentProduct.Price.ToString();
            txtMin.Text = currentProduct.Min.ToString();
            txtMax.Text = currentProduct.Max.ToString();

            associatedParts = new BindingList<Part>(currentProduct.AssociatedParts.ToList());

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
            bool isValid = true;

            ClearErrorStyles();

  
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError(txtName, "Product name cannot be empty.");
                isValid = false;
            }

  
            if (!int.TryParse(txtInventory.Text, out int inventory))
            {
                ShowError(txtInventory, "Inventory must be a valid number.");
                isValid = false;
            }
            else if (inventory < 0)
            {
                ShowError(txtInventory, "Inventory cannot be negative.");
                isValid = false;
            }


            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                ShowError(txtPrice, "Price must be a valid decimal number.");
                isValid = false;
            }
            else if (price <= 0)
            {
                ShowError(txtPrice, "Price must be greater than zero.");
                isValid = false;
            }


            if (!int.TryParse(txtMin.Text, out int min))
            {
                ShowError(txtMin, "Min must be a valid number.");
                isValid = false;
            }

            if (!int.TryParse(txtMax.Text, out int max))
            {
                ShowError(txtMax, "Max must be a valid number.");
                isValid = false;
            }


            if (min > max)
            {
                ShowError(txtMin, "Min cannot be greater than Max.");
                ShowError(txtMax, "Max cannot be less than Min.");
                isValid = false;
            }

  
            if (inventory < min || inventory > max)
            {
                ShowError(txtInventory, $"Inventory must be between {min} and {max}.");
                isValid = false;
            }

            if (isValid)
            {
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

                Inventory.UpdateProduct(currentProduct.ProductID, currentProduct);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please correct the highlighted errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowError(Control control, string message)
        {
            errorProvider.SetError(control, message);

            control.BackColor = System.Drawing.Color.LightSalmon;
        }

        private void ClearErrorStyles()
        {
            errorProvider.Clear();
            txtName.BackColor = System.Drawing.Color.White;
            txtInventory.BackColor = System.Drawing.Color.White;
            txtPrice.BackColor = System.Drawing.Color.White;
            txtMin.BackColor = System.Drawing.Color.White;
            txtMax.BackColor = System.Drawing.Color.White;
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
