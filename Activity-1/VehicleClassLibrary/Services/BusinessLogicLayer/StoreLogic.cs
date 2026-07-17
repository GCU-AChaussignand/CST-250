// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and course-provided examples.

using VehicleClassLibrary.Models;
using VehicleClassLibrary.Services.DataAccessLayer;

namespace VehicleClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Business logic layer that passes data between the presentation layer and data access layer.
    /// </summary>
    public class StoreLogic
    {
        private readonly StoreDAO _storeDAO;

        /// <summary>
        /// Creates the business logic layer and initializes the data access object.
        /// </summary>
        public StoreLogic()
        {
            _storeDAO = new StoreDAO();
        }

        /// <summary>
        /// Returns the inventory list.
        /// </summary>
        public List<VehicleModel> GetInventory()
        {
            return _storeDAO.GetInventory();
        }

        /// <summary>
        /// Returns the shopping cart list.
        /// </summary>
        public List<VehicleModel> GetShoppingCart()
        {
            return _storeDAO.GetShoppingCart();
        }

        /// <summary>
        /// Adds a vehicle to the inventory.
        /// </summary>
        public int AddVehicleToInventory(VehicleModel vehicle)
        {
            return _storeDAO.AddVehicleToInventory(vehicle);
        }

        /// <summary>
        /// Adds a vehicle to the cart by id.
        /// </summary>
        public int AddVehicleToCart(int vehicleId)
        {
            return _storeDAO.AddVehicleToCart(vehicleId);
        }

        /// <summary>
        /// Removes a vehicle from the cart by id.
        /// </summary>
        public bool RemoveVehicleFromCart(int vehicleId)
        {
            return _storeDAO.RemoveVehicleFromCart(vehicleId);
        }

        /// <summary>
        /// Searches inventory using make, model, year, or color.
        /// </summary>
        public List<VehicleModel> SearchInventory(string searchText)
        {
            return _storeDAO.SearchInventory(searchText);
        }

        /// <summary>
        /// Sorts inventory using a selected field.
        /// </summary>
        public void SortInventory(string sortBy)
        {
            _storeDAO.SortInventory(sortBy);
        }

        /// <summary>
        /// Writes inventory to a text file.
        /// </summary>
        public bool WriteInventory()
        {
            return _storeDAO.WriteInventory();
        }

        /// <summary>
        /// Reads inventory from a text file.
        /// </summary>
        public List<VehicleModel> ReadInventory()
        {
            return _storeDAO.ReadInventory();
        }

        /// <summary>
        /// Totals the cart and clears it.
        /// </summary>
        public decimal Checkout()
        {
            return _storeDAO.Checkout();
        }
    }
}
