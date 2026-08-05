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
using PizzaMakerClassLibrary.Services.DataAccessLayer;

namespace PizzaMakerClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Applies pizza validation and coordinates data-access operations.
    /// </summary>
    public class PizzaLogic
    {
        // Declare class level variables
        private readonly PizzaDAO _pizzaDAO;

        /// <summary>
        /// Default constructor for PizzaLogic.
        /// </summary>
        public PizzaLogic()
        {
            // Initialize the pizza DAO object
            _pizzaDAO = new PizzaDAO();
        }

        /// <summary>
        /// Adds a valid pizza to the current order.
        /// </summary>
        /// <param name="newPizza">The pizza submitted by the user.</param>
        /// <returns>A validation result and the number of pizzas in the order.</returns>
        public (bool isValidPizza, int pizzasInOrder) AddPizzaToOrder(PizzaModel newPizza)
        {
            // Validate the pizza before sending it to the DAO
            bool isValidPizza = IsValidPizza(newPizza);
            int pizzasInOrder = _pizzaDAO.GetPizzaOrder().Count;

            if (isValidPizza)
            {
                // Call the DAO AddPizzaToOrder method
                pizzasInOrder = _pizzaDAO.AddPizzaToOrder(newPizza);
            }

            // Return the validation result and order count
            return (isValidPizza, pizzasInOrder);
        }

        /// <summary>
        /// Validates the required pizza fields for the Activity 4 challenge.
        /// </summary>
        /// <param name="pizza">The pizza to validate.</param>
        /// <returns>True when every required value is present.</returns>
        private bool IsValidPizza(PizzaModel pizza)
        {
            return pizza != null
                && !string.IsNullOrWhiteSpace(pizza.ClientName)
                && pizza.ClientName != "Unknown"
                && !string.IsNullOrWhiteSpace(pizza.Crust)
                && pizza.Crust != "Unknown"
                && pizza.Ingredients.Count > 0
                && pizza.SauceQty > 0
                && pizza.CheeseQty > 0;
        }

        /// <summary>
        /// Gets the list of pizzas in the current order.
        /// </summary>
        /// <returns>The current pizza order.</returns>
        public List<PizzaModel> GetPizzaOrder()
        {
            // Get and return GetPizzaOrder from the DAO
            return _pizzaDAO.GetPizzaOrder();
        }

        /// <summary>
        /// Writes the pizza order to a text file.
        /// </summary>
        /// <returns>True when the file is saved successfully.</returns>
        public bool WriteOrderToFile()
        {
            // Get and return WriteOrderToFile from the DAO
            return _pizzaDAO.WriteOrderToFile();
        }
    }
}
