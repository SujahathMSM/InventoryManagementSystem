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
        private ErrorProvider errorProvider = new ErrorProvider();
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
            bool isValid = true;


            ClearErrorStyles();


            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowError(txtName, "Part name cannot be empty.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtInventory.Text))
            {
                ShowError(txtInventory, "Inventory cannot be empty.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                ShowError(txtPrice, "Price cannot be empty.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtMin.Text))
            {
                ShowError(txtMin, "Min cannot be empty.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtMax.Text))
            {
                ShowError(txtMax, "Max cannot be empty.");
                isValid = false;
            }

 
            if (isValid)
            {
                try
                {
                    int inventory = int.Parse(txtInventory.Text);
                    decimal price = decimal.Parse(txtPrice.Text);
                    int min = int.Parse(txtMin.Text);
                    int max = int.Parse(txtMax.Text);

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

           
                    if (rbInHouse.Checked)
                    {
                        int machineID;
                        if (!int.TryParse(txtDynamic.Text, out machineID))
                        {
                            ShowError(txtDynamic, "Machine ID must be a valid number.");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            Part newPart = new InHouse(Inventory.AllParts.Count + 1, txtName.Text, price, inventory, min, max, machineID);
                            Inventory.AddPart(newPart);
                        }
                    }

                    else if (rbOutsourced.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(txtDynamic.Text))
                        {
                            ShowError(txtDynamic, "Company Name cannot be empty.");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            Part newPart = new OutSourced(Inventory.AllParts.Count + 1, txtName.Text, price, inventory, min, max, txtDynamic.Text);
                            Inventory.AddPart(newPart);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select either InHouse or Outsourced.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isValid = false;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please ensure all numeric fields are properly filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isValid = false;
                }
            }


            if (isValid)
            {
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


            control.BackColor = Color.LightSalmon;
        }

        private void ClearErrorStyles()
        {

            errorProvider.Clear();
            txtName.BackColor = Color.White;
            txtInventory.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtMin.BackColor = Color.White;
            txtMax.BackColor = Color.White;
            txtDynamic.BackColor = Color.White;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
