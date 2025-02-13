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
using System.Xml.Linq;

namespace InventoryManagementSystem.Forms
{
    public partial class AddPartForm : Form
    {
        public AddPartForm()
        {
            InitializeComponent();
        }

        private void rbInHouse_CheckedChanged(object sender, EventArgs e)
        {
            lblDynamic.Text = "Machine ID";
            txtDynamic.Visible = true;
        }

        private void rbOutsourced_CheckedChanged(object sender, EventArgs e)
        {
            lblDynamic.Text = "Company Name";
            txtDynamic.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtInventory.Text) ||
                    string.IsNullOrWhiteSpace(txtPrice.Text) ||
                    string.IsNullOrWhiteSpace(txtMin.Text) ||
                    string.IsNullOrWhiteSpace(txtMax.Text))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert input values
                int inventory = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int min = int.Parse(txtMin.Text);
                int max = int.Parse(txtMax.Text);

                if (min > max || inventory < min || inventory > max)
                {
                    MessageBox.Show("Inventory must be between Min and Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Determine part type
                Part newPart;
                if (rbInHouse.Checked)
                {
                    int machineID;
                    if (!int.TryParse(txtDynamic.Text, out machineID))
                    {
                        MessageBox.Show("Machine ID must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    newPart = new InHouse(Inventory.AllParts.Count + 1, txtName.Text, price, inventory, min, max, machineID);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtDynamic.Text))
                    {
                        MessageBox.Show("Company Name must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    newPart = new OutSourced(Inventory.AllParts.Count + 1, txtName.Text, price, inventory, min, max, txtDynamic.Text);
                }

                // Add part to inventory
                Inventory.AddPart(newPart);

                // Close form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving part: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
