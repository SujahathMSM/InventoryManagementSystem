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
    public partial class AddProductForm : Form
    {
        private BindingList<Part> associatedParts = new BindingList<Part>();
        public AddProductForm()
        {
            InitializeComponent();
            this.Load += AddProductForm_Load;
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            {

                dgvAllCandidateParts.AutoGenerateColumns = true;


                dgvAllCandidateParts.DataSource = Inventory.AllParts;


                dgvAssociatedParts.AutoGenerateColumns = true;
                dgvAssociatedParts.DataSource = associatedParts;
            }
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

            // Clear previous error styles
            ClearErrorStyles();

            // Validate Name (ensure it's not empty)
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError(txtName, "Product name cannot be empty.");
                isValid = false;
            }

            // Validate Inventory
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

            // Validate Price
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

            // Validate Min and Max
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

            // Ensure Min is less than or equal to Max
            if (min > max)
            {
                ShowError(txtMin, "Min cannot be greater than Max.");
                ShowError(txtMax, "Max cannot be less than Min.");
                isValid = false;
            }

            // Ensure Inventory is between Min and Max
            if (inventory < min || inventory > max)
            {
                ShowError(txtInventory, $"Inventory must be between {min} and {max}.");
                isValid = false;
            }

            // If all validations passed, create the product
            if (isValid)
            {
                Product newProduct = new Product
                {
                    ProductID = Inventory.Products.Count + 1, // Assuming this is how ProductID is assigned
                    Name = txtName.Text,
                    InStock = inventory,
                    Price = price,
                    Min = min,
                    Max = max
                };

                foreach (Part part in associatedParts)
                {
                    newProduct.AddAssociatedPart(part);
                }

                Inventory.AddProduct(newProduct);

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
            // Show error message next to the field (using a label or tooltip)
            errorProvider.SetError(control, message);

            // Change the background color of the field to indicate error
            control.BackColor = Color.LightSalmon;
        }

        private void ClearErrorStyles()
        {
            // Clear all error styles and reset field colors
            errorProvider.Clear();
            txtName.BackColor = Color.White;
            txtInventory.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtMin.BackColor = Color.White;
            txtMax.BackColor = Color.White;
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
