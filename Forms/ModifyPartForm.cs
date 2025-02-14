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
        private ErrorProvider errorProvider = new ErrorProvider();
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


                    if (rbInHouse.Checked && selectedPart is InHouse inHousePart)
                    {
                        int machineID;
                        if (!int.TryParse(txtDynamic.Text, out machineID))
                        {
                            ShowError(txtDynamic, "Machine ID must be a valid number.");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            inHousePart.MachineID = machineID;
                        }
                    }

                    else if (rbOutsourced.Checked && selectedPart is OutSourced outsourcedPart)
                    {
                        if (string.IsNullOrWhiteSpace(txtDynamic.Text))
                        {
                            ShowError(txtDynamic, "Company Name cannot be empty.");
                            isValid = false;
                        }

                        if (isValid)
                        {
                            outsourcedPart.CompanyName = txtDynamic.Text;
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
                selectedPart.Name = txtName.Text;
                selectedPart.Price = decimal.Parse(txtPrice.Text);
                selectedPart.InStock = int.Parse(txtInventory.Text);
                selectedPart.Min = int.Parse(txtMin.Text);
                selectedPart.Max = int.Parse(txtMax.Text);

                Inventory.UpdatePart(selectedPart.PartID, selectedPart);

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
        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
