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
    public partial class ModifyPartForm : Form
    {
        private Part selectedPart;
        public ModifyPartForm(Part part)
        {
            InitializeComponent();
            selectedPart = part;
            LoadPartData();
        }

        private void LoadPartData()
        {
            txtID.Text = selectedPart.PartID.ToString();
            txtName.Text = selectedPart.Name;
            txtInventory.Text = selectedPart.InStock.ToString();
            txtPrice.Text = selectedPart.Price.ToString();
            txtMin.Text = selectedPart.Min.ToString();
            txtMax.Text = selectedPart.Max.ToString();

            if (selectedPart is InHouse inHousePart)
            {
                rbInHouse.Checked = true;
                txtDynamic.Text = inHousePart.MachineID.ToString();
                lblDynamic.Text = "Machine ID";
            }
            else if (selectedPart is OutSourced outsourcedPart)
            {
                rbOutsourced.Checked = true;
                txtDynamic.Text = outsourcedPart.CompanyName;
                lblDynamic.Text = "Company Name";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

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
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtInventory.Text) ||
                    string.IsNullOrWhiteSpace(txtPrice.Text) ||
                    string.IsNullOrWhiteSpace(txtMin.Text) ||
                    string.IsNullOrWhiteSpace(txtMax.Text))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int inventory = int.Parse(txtInventory.Text);
                decimal price = decimal.Parse(txtPrice.Text);
                int min = int.Parse(txtMin.Text);
                int max = int.Parse(txtMax.Text);

                if (min > max || inventory < min || inventory > max)
                {
                    MessageBox.Show("Inventory must be between Min and Max.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selectedPart.Name = txtName.Text;
                selectedPart.Price = price;
                selectedPart.InStock = inventory;
                selectedPart.Min = min;
                selectedPart.Max = max;

                if (rbInHouse.Checked && selectedPart is InHouse inHousePart)
                {
                    inHousePart.MachineID = int.Parse(txtDynamic.Text);
                }
                else if (rbOutsourced.Checked && selectedPart is OutSourced outsourcedPart)
                {
                    outsourcedPart.CompanyName = txtDynamic.Text;
                }

                Inventory.UpdatePart(selectedPart.PartID, selectedPart);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating part: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
