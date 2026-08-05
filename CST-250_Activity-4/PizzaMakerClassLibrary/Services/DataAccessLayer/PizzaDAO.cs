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

using System.Text;
using PizzaMakerClassLibrary.Models;

namespace PizzaMakerClassLibrary.Services.DataAccessLayer
{
    /// <summary>
    /// Stores the current pizza order and writes it to a text file.
    /// </summary>
    public class PizzaDAO
    {
        // Class level variables
        private readonly List<PizzaModel> _pizzaOrder;

        /// <summary>
        /// Default constructor for the pizza DAO.
        /// </summary>
        public PizzaDAO()
        {
            // Initialize the _pizzaOrder list
            _pizzaOrder = new List<PizzaModel>();
        }

        /// <summary>
        /// Adds a pizza to the current order.
        /// </summary>
        /// <param name="newPizza">The pizza to add.</param>
        /// <returns>The number of pizzas in the current order.</returns>
        public int AddPizzaToOrder(PizzaModel newPizza)
        {
            // Add the new pizza to the pizza order list
            _pizzaOrder.Add(newPizza);

            // Return the number of pizzas in the order
            return _pizzaOrder.Count;
        }

        /// <summary>
        /// Gets the list of pizzas in the current order.
        /// </summary>
        /// <returns>The current pizza order.</returns>
        public List<PizzaModel> GetPizzaOrder()
        {
            return _pizzaOrder;
        }

        /// <summary>
        /// Writes the pizza order to a text file.
        /// </summary>
        /// <returns>True when the file is saved successfully; otherwise, false.</returns>
        public bool WriteOrderToFile()
        {
            // Declare and initialize
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            string pizzaString;

            // Check if the directory exists
            if (!Directory.Exists(filePath))
            {
                // Create the directory
                Directory.CreateDirectory(filePath);
            }

            // Set up a try-catch for the file writer
            try
            {
                // Create a using statement for StreamWriter
                using (StreamWriter streamWriter = new StreamWriter(
                    Path.Combine(filePath, "PizzaOrder.txt"), false, Encoding.UTF8))
                {
                    // Loop through the pizza order list
                    foreach (PizzaModel pizza in _pizzaOrder)
                    {
                        pizzaString =
                            $"Name: {pizza.ClientName}\n" +
                            $"Ingredients: {string.Join(", ", pizza.Ingredients)}\n" +
                            $"Strange Add Ons: {string.Join(", ", pizza.StrangeAddOns)}\n" +
                            $"Crust: {pizza.Crust}\n" +
                            $"Sauce: {pizza.SauceQty}%\n" +
                            $"Cheese: {pizza.CheeseQty}%\n" +
                            $"Delivery Time: {pizza.DeliveryTime:g}\n" +
                            $"Pizza Box Color: {pizza.PizzaBoxColor.Name}\n" +
                            $"Price: {pizza.Price:C2}\n";

                        streamWriter.WriteLine(pizzaString);
                    }
                }

                // Return true
                return true;
            }
            catch
            {
                // Return false
                return false;
            }
        }
    }
}
