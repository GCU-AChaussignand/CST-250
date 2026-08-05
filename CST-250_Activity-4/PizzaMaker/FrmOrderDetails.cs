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
    /// Presentation layer for displaying and saving the current order.
    /// </summary>
    public partial class FrmOrderDetails : Form
    {
        // Declare class level variables
        private readonly List<PizzaModel> _pizzaOrders;
        private readonly PizzaLogic _pizzaLogic;

        /// <summary>
        /// Default constructor for FrmOrderDetails.
        /// </summary>
        public FrmOrderDetails()
        {
            InitializeComponent();
            _pizzaOrders = new List<PizzaModel>();
            _pizzaLogic = new PizzaLogic();
        }

        /// <summary>
        /// Parameterized constructor for FrmOrderDetails.
        /// </summary>
        /// <param name="pizzaOrderList">The pizzas in the current order.</param>
        /// <param name="pizzaBusinessLogic">The shared business-logic object.</param>
        public FrmOrderDetails(List<PizzaModel> pizzaOrderList, PizzaLogic pizzaBusinessLogic)
        {
            // Initialize the form
            InitializeComponent();

            // Initialize the class level variables
            _pizzaOrders = pizzaOrderList;
            _pizzaLogic = pizzaBusinessLogic;
        }

        /// <summary>
        /// Displays the pizzas on the form.
        /// </summary>
        public void DisplayPizzas()
        {
            // Clear the label
            lblOrderDetails.Text = string.Empty;

            // Loop through the pizza order list
            foreach (PizzaModel pizza in _pizzaOrders)
            {
                lblOrderDetails.Text +=
                    $"Name: {pizza.ClientName}\n" +
                    $"Ingredients: {string.Join(", ", pizza.Ingredients)}\n" +
                    $"Strange Add Ons: {string.Join(", ", pizza.StrangeAddOns)}\n" +
                    $"Crust: {pizza.Crust}\n" +
                    $"Sauce: {pizza.SauceQty}%\n" +
                    $"Cheese: {pizza.CheeseQty}%\n" +
                    $"Delivery Time: {pizza.DeliveryTime:g}\n" +
                    $"Pizza Box Color: {pizza.PizzaBoxColor.Name}\n" +
                    $"Price: {pizza.Price:C2}\n\n";
            }
        }

        /// <summary>
        /// Click event handler for btnSaveOrder.
        /// </summary>
        private void BtnSaveOrderClickEH(object sender, EventArgs e)
        {
            // Declare and initialize
            bool isSaveSuccess = _pizzaLogic.WriteOrderToFile();

            // Check if the save was successful
            if (isSaveSuccess)
            {
                MessageBox.Show(
                    "The pizza order was saved.",
                    "Order Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "An error occurred while trying to save your order. Please try again later.",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Click event handler for the Activity 4 challenge Back button.
        /// </summary>
        private void BtnBackClickEH(object sender, EventArgs e)
        {
            // Close the modal form and return to the existing Pizza Maker form
            Close();
        }
    }
}
