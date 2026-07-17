// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and course-provided examples.

namespace VehicleClassLibrary.Models
{
    /// <summary>
    /// Base model for all vehicles in the store inventory.
    /// </summary>
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int NumWheels { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }

        /// <summary>
        /// Creates a default vehicle.
        /// </summary>
        public VehicleModel()
        {
            Id = 0;
            Make = "Unknown";
            Model = "Unknown";
            Year = 2000;
            Price = 0.00m;
            NumWheels = 4;
            Color = "Unknown";
            Mileage = 0;
        }

        /// <summary>
        /// Creates a vehicle with the original six assigned properties.
        /// </summary>
        public VehicleModel(int id, string make, string model, int year, decimal price, int numWheels)
            : this(id, make, model, year, price, numWheels, "Unknown", 0)
        {
        }

        /// <summary>
        /// Creates a vehicle with all common properties, including challenge properties.
        /// </summary>
        public VehicleModel(int id, string make, string model, int year, decimal price, int numWheels, string color, int mileage)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            Price = price;
            NumWheels = numWheels;
            Color = color;
            Mileage = mileage;
        }

        /// <summary>
        /// Formats a vehicle for console, GUI, and debugging output.
        /// </summary>
        public override string ToString()
        {
            return $"#{Id}: {Year} {Make} {Model} | Price: {Price:C2} | Wheels: {NumWheels} | Color: {Color} | Mileage: {Mileage:n0}";
        }

        /// <summary>
        /// Compares vehicles to prevent duplicate inventory entries.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is not VehicleModel vehicle)
            {
                return false;
            }

            return GetType() == vehicle.GetType()
                && Make.Equals(vehicle.Make, StringComparison.OrdinalIgnoreCase)
                && Model.Equals(vehicle.Model, StringComparison.OrdinalIgnoreCase)
                && Year == vehicle.Year
                && Price == vehicle.Price
                && NumWheels == vehicle.NumWheels
                && Color.Equals(vehicle.Color, StringComparison.OrdinalIgnoreCase)
                && Mileage == vehicle.Mileage;
        }

        /// <summary>
        /// Returns a hash code compatible with the Equals override.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Make.ToLowerInvariant(), Model.ToLowerInvariant(), Year, Price, NumWheels, Color.ToLowerInvariant(), Mileage);
        }
    }
}
