//Team members: Hanna Lawrence, Rani Madhiwala, Peyton Frye, Kai Kang
//10/29/20
//Minor Assignment 3
//The purpose of this app is to read in and compute commands based off the products database.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinorAssignment3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // This line of code loads data into the 'productdatabase.product' table.
            this.productTableAdapter1.Fill(this.productDatabaseDataSet1.PRODUCT);
            productNameComboBox.Focus();
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearLabels()
        {
            //method with parameters to clear outputs
            inventoryLabel.Text = null;
        }
        private void inventoryLabel_TextChanged(object sender, EventArgs e)
        {
            ClearLabels();
        }

        private void productNameComboBox_Enter(object sender, EventArgs e)
        {
            productNameComboBox.SelectAll();
        }

        private void costTextBox_Enter(object sender, EventArgs e)
        {
            costTextBox.SelectAll();
        }

        private void quantityTextBox_Enter(object sender, EventArgs e)
        {
            quantityTextBox.SelectAll();
        }

        private void productNameComboBox_TextChanged(object sender, EventArgs e)
        {
            quantityTextBox.Text = null;
            costTextBox.Text = null;
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            // declare variables
            double productCost, quantity, inventoryValue = 0, inventoryPercentage = 0, cumulativePercentage =0, inventoryTotal = 0, inventoryPercentageTotal = 0;
            string productName;

            //Clear the listview
            productListView.Items.Clear();
            /*Load the listview by reading through each row of the product table, copying three values
            to an array, and moving the array to the listview row */
            foreach (DataRow dr in this.productDatabaseDataSet1.PRODUCT.Rows)
            {
                productName = dr["Name"].ToString();
                productCost = double.Parse(dr["Product Cost"].ToString());
                quantity = double.Parse(dr["Quantity"].ToString());
                inventoryValue = productCost * quantity;
                inventoryValue = double.Parse(dr["Inventory Value"].ToString());
                inventoryTotal += inventoryValue; //accumulates the inventory values so that can calculate inventory percentage
                inventoryPercentage = ((inventoryValue / inventoryTotal) * 100);
                inventoryPercentage = double.Parse(dr["Inventory %"].ToString());
                inventoryPercentageTotal += inventoryPercentage; //accumulates the inventory %  so that can calculate cumulative %
                cumulativePercentage += inventoryPercentageTotal + inventoryPercentage;
                //Create a listview to show all products and purchase amounts
                string[] fieldsArray = new string[6];
                fieldsArray[0] = productName;
                fieldsArray[1] = productCost.ToString("N");
                fieldsArray[2] = quantity.ToString("D");
                fieldsArray[3] = inventoryValue.ToString("C");
                fieldsArray[4] = inventoryPercentage.ToString("P");
                fieldsArray[5] = cumulativePercentage.ToString("P");
                ListViewItem productsLVI = new ListViewItem(fieldsArray);
                productListView.Items.Add(productsLVI);
            }
            //display total inventory value in labels
            inventoryLabel.Text = inventoryTotal.ToString("C");

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
                this.Close();
        }

        private void productbindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }
    }
}
