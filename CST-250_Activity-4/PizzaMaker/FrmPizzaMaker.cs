/*
 * Aaron Chaussignand
 * Grand Canyon University
 * CST-250: Programming in C# II
 * Instructor: Brian Grey
 * Activity 4 - Pizza Maker
 * August 4, 2026
 *
 * Source: Grand Canyon University. (2025). CST-250 Activity 4:
 * Building a Data-Driven Pizza Order System with N-Layer Architecture.
 */

using PizzaMakerClassLibrary.Models;
using PizzaMakerClassLibrary.Services.BusinessLogicLayer;

namespace PizzaMaker
{
    /// <summary>
    /// Presentation layer for creating pizzas.
    /// </summary>
    public partial class FrmPizzaMaker : Form
    {
        // Class level variable declarations
        private PizzaModel _pizza;
        private readonly PizzaLogic _pizzaLogic;

        /// <summary>
        /// Default constructor for FrmPizzaMaker.
        /// </summary>
        public FrmPizzaMaker()
        {
            InitializeComponent();

            // Initialize the current order and business logic layer
            _pizza = new PizzaModel();
            _pizzaLogic = new PizzaLogic();

            // Disable buttons until the user enters a name or creates an order
            btnCreatePizza.Enabled = false;
            btnResetForm.Enabled = false;
            btnSeeFullOrder.Enabled = false;

            // Show the base pizza price
            UpdatePrice();

            // Correct the HScrollBar effective maximum so 100 can be selected
            hsbSauce.Maximum = 100 + hsbSauce.LargeChange - 1;
            hsbCheese.Maximum = 100 + hsbCheese.LargeChange - 1;

            // Initialize controls from the current model
            dtpDeliveryTime.Value = _pizza.DeliveryTime;
            picPizzaBoxColor.BackColor = _pizza.PizzaBoxColor;
        }

        /// <summary>
        /// Enables the create and reset form buttons.
        /// </summary>
        private void EnablePizzaCreation()
        {
            btnCreatePizza.Enabled = true;
            btnResetForm.Enabled = true;
        }

        /// <summary>
        /// Leave event handler for txtName.
        /// </summary>
        private void TxtNameLeaveEH(object sender, EventArgs e)
        {
            // Set the pizza client name to the text of txtName
            _pizza.ClientName = txtName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(_pizza.ClientName))
            {
                EnablePizzaCreation();
            }
            else
            {
                btnCreatePizza.Enabled = false;
                btnResetForm.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the price of the pizza.
        /// </summary>
        private void UpdatePrice()
        {
            // Declare and initialize
            decimal price = 15m;

            // Add $0.50 for each ingredient
            price += _pizza.Ingredients.Count * 0.50m;

            // Add $0.50 for each special add on
            price += _pizza.StrangeAddOns.Count * 0.50m;

            // Add $1 if the crust is gluten free
            if (_pizza.Crust == "Gluten Free")
            {
                price += 1m;
            }

            // Update the price of the pizza and the label
            _pizza.Price = price;
            lblPizzaPrice.Text = price.ToString("C2");
        }

        /// <summary>
        /// Checked changed event handler for ingredient check boxes.
        /// </summary>
        private void ChbIngredientCheckedChangedEH(object sender, EventArgs e)
        {
            // Get the check box from the sender parameter
            CheckBox checkbox = sender as CheckBox;

            // Make sure the checkbox is not null
            if (checkbox != null)
            {
                // If checked, add the ingredient to the pizza
                if (checkbox.Checked)
                {
                    if (!_pizza.Ingredients.Contains(checkbox.Text))
                    {
                        _pizza.Ingredients.Add(checkbox.Text);
                    }
                }
                else
                {
                    // If unchecked, remove the ingredient
                    _pizza.Ingredients.Remove(checkbox.Text);
                }

                // Update the price of the pizza
                UpdatePrice();
            }
        }

        /// <summary>
        /// Selected index changed event handler for lsbStrangeAddOns.
        /// </summary>
        private void LsbStrangeAddOnsSelectedIndexChangedEH(object sender, EventArgs e)
        {
            // Get the selected list items and set the StrangeAddOns property
            _pizza.StrangeAddOns = lsbStrangeAddOns.SelectedItems.Cast<string>().ToList();
            UpdatePrice();
        }

        /// <summary>
        /// Checked changed event handler for crust radio buttons.
        /// </summary>
        private void RdoCrustCheckedChangedEH(object sender, EventArgs e)
        {
            // Get the radio button from the sender object
            RadioButton radioButton = sender as RadioButton;

            // Make sure the radio button is not null and is checked
            if (radioButton != null && radioButton.Checked)
            {
                // Set the current crust as the pizza's crust
                _pizza.Crust = radioButton.Text;
                UpdatePrice();
            }
        }

        /// <summary>
        /// Value changed event handler for the horizontal scroll bars.
        /// </summary>
        private void HsbExtraGoodiesValueChangedEH(object sender, EventArgs e)
        {
            // Cast the sender as an HScrollBar
            HScrollBar scrollBar = sender as HScrollBar;

            if (scrollBar != null)
            {
                // Determine which scroll bar called the method
                if (scrollBar == hsbSauce)
                {
                    _pizza.SauceQty = scrollBar.Value;
                    lblSauce.Text = scrollBar.Value.ToString();
                }
                else if (scrollBar == hsbCheese)
                {
                    _pizza.CheeseQty = scrollBar.Value;
                    lblCheese.Text = scrollBar.Value.ToString();
                }
            }
        }

        /// <summary>
        /// Value changed event handler for dtpDeliveryTime.
        /// </summary>
        private void DtpDeliveryTimeValueChangedEH(object sender, EventArgs e)
        {
            // Update the delivery time for the pizza
            _pizza.DeliveryTime = dtpDeliveryTime.Value;
        }

        /// <summary>
        /// Click event handler for picPizzaBoxColor.
        /// </summary>
        private void PicPizzaBoxColorClickEH(object sender, EventArgs e)
        {
            // Create a new color dialog object
            using ColorDialog pizzaBoxColorPicker = new ColorDialog();
            pizzaBoxColorPicker.Color = picPizzaBoxColor.BackColor;

            // Call ShowDialog and save the result
            DialogResult result = pizzaBoxColorPicker.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Set the pizza box color and update the picture box
                _pizza.PizzaBoxColor = pizzaBoxColorPicker.Color;
                picPizzaBoxColor.BackColor = pizzaBoxColorPicker.Color;
            }
        }

        /// <summary>
        /// Click event handler for btnResetForm.
        /// </summary>
        private void BtnResetFormClickEH(object sender, EventArgs e)
        {
            ResetForm();
        }

        /// <summary>
        /// Resets the pizza maker form.
        /// </summary>
        private void ResetForm()
        {
            // Reset the pizza to a new instance
            _pizza = new PizzaModel();

            // Reset the controls of the form
            ResetControls(this);

            // Restore current delivery time and default pizza-box color
            dtpDeliveryTime.Value = _pizza.DeliveryTime;
            picPizzaBoxColor.BackColor = _pizza.PizzaBoxColor;

            // Disable buttons that require form input
            btnCreatePizza.Enabled = false;
            btnResetForm.Enabled = false;

            // Update the price of the pizza
            UpdatePrice();
        }

        /// <summary>
        /// Resets controls within the parent control.
        /// </summary>
        /// <param name="parentControl">The form or container to reset.</param>
        private void ResetControls(Control parentControl)
        {
            // Loop through the controls within the parent control
            foreach (Control control in parentControl.Controls)
            {
                // Use a switch statement to reset controls based on type
                switch (control)
                {
                    case TextBox textBox:
                        textBox.Clear();
                        break;
                    case CheckBox checkBox:
                        checkBox.Checked = false;
                        break;
                    case ListBox listBox:
                        listBox.ClearSelected();
                        break;
                    case RadioButton radioButton:
                        radioButton.Checked = false;
                        break;
                    case HScrollBar horizontalScrollBar:
                        horizontalScrollBar.Value = horizontalScrollBar.Minimum;
                        break;
                    case DateTimePicker dateTimePicker:
                        dateTimePicker.Value = DateTime.Now;
                        break;
                    case PictureBox pictureBox:
                        pictureBox.BackColor = Color.White;
                        break;
                }

                // Recursively call the reset method for child controls
                if (control.HasChildren)
                {
                    ResetControls(control);
                }
            }
        }

        /// <summary>
        /// Click event handler for btnCreatePizza.
        /// </summary>
        private void BtnCreatePizzaClickEH(object sender, EventArgs e)
        {
            // Declare and initialize
            bool isValidPizza;
            int pizzasInOrder;

            // Use the pizza logic to add the current pizza
            (isValidPizza, pizzasInOrder) = _pizzaLogic.AddPizzaToOrder(_pizza);

            if (isValidPizza)
            {
                // Enable the See Full Order button and reset the form
                btnSeeFullOrder.Enabled = true;
                ResetForm();
            }
            else
            {
                MessageBox.Show(
                    "Your pizza order is not complete. Enter a name, select a crust and at least one ingredient, and set sauce and cheese above 0.",
                    "Incomplete Pizza",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Click event handler for btnSeeFullOrder.
        /// </summary>
        private void BtnSeeFullOrderClickEH(object sender, EventArgs e)
        {
            // Declare and initialize the current pizza list
            List<PizzaModel> pizzaList = _pizzaLogic.GetPizzaOrder();

            // Create a new form with the pizza list and shared business logic
            using FrmOrderDetails frmOrderDetails = new FrmOrderDetails(pizzaList, _pizzaLogic);
            frmOrderDetails.DisplayPizzas();
            frmOrderDetails.ShowDialog(this);
        }
    }
}
