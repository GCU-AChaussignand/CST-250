// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, Microsoft C# documentation.

using System.Globalization;
using VehicleClassLibrary.Models;

namespace VehicleClassLibrary.Services.DataAccessLayer
{
    /// <summary>
    /// Data access object that stores inventory and shopping cart data.
    /// </summary>
    public class StoreDAO
    {
        private readonly List<VehicleModel> _inventory;
        private readonly List<VehicleModel> _shoppingCart;
        private readonly string _dataDirectory;
        private readonly string _fileName;
        private readonly string _filePath;

        /// <summary>
        /// Creates the data access object and initializes data lists and file paths.
        /// </summary>
        public StoreDAO()
        {
            _inventory = new List<VehicleModel>();
            _shoppingCart = new List<VehicleModel>();
            _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _fileName = "inventory.txt";
            _filePath = Path.Combine(_dataDirectory, _fileName);
        }

        /// <summary>
        /// Returns the current store inventory list.
        /// </summary>
        public List<VehicleModel> GetInventory()
        {
            return _inventory;
        }

        /// <summary>
        /// Returns the current shopping cart list.
        /// </summary>
        public List<VehicleModel> GetShoppingCart()
        {
            return _shoppingCart;
        }

        /// <summary>
        /// Adds a new vehicle to the inventory and returns the assigned id.
        /// </summary>
        public int AddVehicleToInventory(VehicleModel vehicle)
        {
            if (vehicle == null)
            {
                return -1;
            }

            if (_inventory.Any(existingVehicle => existingVehicle.Equals(vehicle)))
            {
                return 0;
            }

            vehicle.Id = _inventory.Count + 1;
            _inventory.Add(vehicle);
            return vehicle.Id;
        }

        /// <summary>
        /// Adds a vehicle from inventory to the shopping cart by inventory id.
        /// </summary>
        public int AddVehicleToCart(int vehicleId)
        {
            VehicleModel? selectedVehicle = _inventory.FirstOrDefault(vehicle => vehicle.Id == vehicleId);

            if (selectedVehicle == null)
            {
                return -1;
            }

            _shoppingCart.Add(selectedVehicle);
            return _shoppingCart.Count;
        }

        /// <summary>
        /// Removes a selected vehicle from the shopping cart by id.
        /// </summary>
        public bool RemoveVehicleFromCart(int vehicleId)
        {
            VehicleModel? selectedVehicle = _shoppingCart.FirstOrDefault(vehicle => vehicle.Id == vehicleId);

            if (selectedVehicle == null)
            {
                return false;
            }

            _shoppingCart.Remove(selectedVehicle);
            return true;
        }

        /// <summary>
        /// Searches inventory by make, model, year, or color.
        /// </summary>
        public List<VehicleModel> SearchInventory(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new List<VehicleModel>(_inventory);
            }

            string search = searchText.Trim().ToLowerInvariant();
            return _inventory
                .Where(vehicle => vehicle.Make.ToLowerInvariant().Contains(search)
                    || vehicle.Model.ToLowerInvariant().Contains(search)
                    || vehicle.Color.ToLowerInvariant().Contains(search)
                    || vehicle.Year.ToString(CultureInfo.InvariantCulture).Contains(search))
                .ToList();
        }

        /// <summary>
        /// Sorts inventory by make, model, year, or price.
        /// </summary>
        public void SortInventory(string sortBy)
        {
            switch (sortBy.Trim().ToLowerInvariant())
            {
                case "make":
                    _inventory.Sort((left, right) => string.Compare(left.Make, right.Make, StringComparison.OrdinalIgnoreCase));
                    break;
                case "model":
                    _inventory.Sort((left, right) => string.Compare(left.Model, right.Model, StringComparison.OrdinalIgnoreCase));
                    break;
                case "year":
                    _inventory.Sort((left, right) => left.Year.CompareTo(right.Year));
                    break;
                case "price":
                    _inventory.Sort((left, right) => left.Price.CompareTo(right.Price));
                    break;
                default:
                    _inventory.Sort((left, right) => left.Id.CompareTo(right.Id));
                    break;
            }
        }

        /// <summary>
        /// Writes the current inventory to a text file.
        /// </summary>
        public bool WriteInventory()
        {
            try
            {
                if (!Directory.Exists(_dataDirectory))
                {
                    Directory.CreateDirectory(_dataDirectory);
                }

                using StreamWriter writer = new StreamWriter(_filePath, false);

                foreach (VehicleModel vehicle in _inventory)
                {
                    string line = ConvertVehicleToFileLine(vehicle);
                    writer.WriteLine(line);
                }

                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        } // End WriteInventory

        /// <summary>
        /// Reads inventory from the text file and returns the updated inventory list.
        /// </summary>
        public List<VehicleModel> ReadInventory()
        {
            if (!File.Exists(_filePath))
            {
                return _inventory;
            }

            try
            {
                _inventory.Clear();

                using StreamReader reader = new StreamReader(_filePath);
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    VehicleModel? vehicle = ConvertFileLineToVehicle(line);

                    if (vehicle != null)
                    {
                        AddVehicleToInventory(vehicle);
                    }
                }
            }
            catch (IOException)
            {
                return _inventory;
            }
            catch (UnauthorizedAccessException)
            {
                return _inventory;
            }

            return _inventory;
        } // End ReadInventory

        /// <summary>
        /// Safely parses an integer, returning 0 when parsing fails.
        /// </summary>
        private int ParseInteger(string value)
        {
            try
            {
                return int.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Safely parses a decimal, returning 0 when parsing fails.
        /// </summary>
        private decimal ParseDecimal(string value)
        {
            try
            {
                return decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return 0.0m;
            }
            catch (OverflowException)
            {
                return 0.0m;
            }
        }

        /// <summary>
        /// Safely parses a Boolean, returning false when parsing fails.
        /// </summary>
        private bool ParseBoolean(string value)
        {
            try
            {
                return bool.Parse(value);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Totals all cart prices, empties the shopping cart, and returns the total.
        /// </summary>
        public decimal Checkout()
        {
            decimal total = 0.0m;

            foreach (VehicleModel vehicle in _shoppingCart)
            {
                total += vehicle.Price;
            }

            _shoppingCart.Clear();
            return total;
        }

        /// <summary>
        /// Converts a vehicle object into a file line.
        /// </summary>
        private static string ConvertVehicleToFileLine(VehicleModel vehicle)
        {
            string commonData = string.Join('|',
                vehicle.GetType().Name,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year.ToString(CultureInfo.InvariantCulture),
                vehicle.Price.ToString(CultureInfo.InvariantCulture),
                vehicle.NumWheels.ToString(CultureInfo.InvariantCulture),
                vehicle.Color,
                vehicle.Mileage.ToString(CultureInfo.InvariantCulture));

            return vehicle switch
            {
                CarModel car => $"{commonData}|{car.IsConvertible}|{car.TrunkSize.ToString(CultureInfo.InvariantCulture)}",
                MotorcycleModel motorcycle => $"{commonData}|{motorcycle.HasSideCar}|{motorcycle.SeatHeight.ToString(CultureInfo.InvariantCulture)}",
                PickupModel pickup => $"{commonData}|{pickup.HasBedCover}|{pickup.BedSize.ToString(CultureInfo.InvariantCulture)}",
                _ => commonData
            };
        }

        /// <summary>
        /// Converts a saved text file line into a vehicle object.
        /// </summary>
        private VehicleModel? ConvertFileLineToVehicle(string line)
        {
            string[] parts = line.Split('|');

            if (parts.Length < 8)
            {
                return null;
            }

            string type = parts[0];
            string make = parts[1];
            string model = parts[2];
            int year = ParseInteger(parts[3]);
            decimal price = ParseDecimal(parts[4]);
            int numWheels = ParseInteger(parts[5]);
            string color = parts[6];
            int mileage = ParseInteger(parts[7]);

            if (type == nameof(CarModel) && parts.Length >= 10)
            {
                bool isConvertible = ParseBoolean(parts[8]);
                decimal trunkSize = ParseDecimal(parts[9]);
                return new CarModel(0, make, model, year, price, numWheels, color, mileage, isConvertible, trunkSize);
            }

            if (type == nameof(MotorcycleModel) && parts.Length >= 10)
            {
                bool hasSideCar = ParseBoolean(parts[8]);
                decimal seatHeight = ParseDecimal(parts[9]);
                return new MotorcycleModel(0, make, model, year, price, numWheels, color, mileage, hasSideCar, seatHeight);
            }

            if (type == nameof(PickupModel) && parts.Length >= 10)
            {
                bool hasBedCover = ParseBoolean(parts[8]);
                decimal bedSize = ParseDecimal(parts[9]);
                return new PickupModel(0, make, model, year, price, numWheels, color, mileage, hasBedCover, bedSize);
            }

            return new VehicleModel(0, make, model, year, price, numWheels, color, mileage);
        }
    }
}
