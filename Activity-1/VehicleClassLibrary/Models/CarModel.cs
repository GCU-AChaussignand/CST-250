// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and course-provided examples.

namespace VehicleClassLibrary.Models
{
    /// <summary>
    /// Specialized vehicle model for cars.
    /// </summary>
    public class CarModel : VehicleModel
    {
        public bool IsConvertible { get; set; }
        public decimal TrunkSize { get; set; }

        /// <summary>
        /// Creates a default car.
        /// </summary>
        public CarModel()
            : base()
        {
            IsConvertible = false;
            TrunkSize = 0.0m;
        }

        /// <summary>
        /// Creates a car with original common properties and car-specific properties.
        /// </summary>
        public CarModel(int id, string make, string model, int year, decimal price, int numWheels, bool isConvertible, decimal trunkSize)
            : base(id, make, model, year, price, numWheels)
        {
            IsConvertible = isConvertible;
            TrunkSize = trunkSize;
        }

        /// <summary>
        /// Creates a car with all common properties and car-specific properties.
        /// </summary>
        public CarModel(int id, string make, string model, int year, decimal price, int numWheels, string color, int mileage, bool isConvertible, decimal trunkSize)
            : base(id, make, model, year, price, numWheels, color, mileage)
        {
            IsConvertible = isConvertible;
            TrunkSize = trunkSize;
        }

        /// <summary>
        /// Formats a car for display.
        /// </summary>
        public override string ToString()
        {
            string convertibleText = IsConvertible ? "Yes" : "No";
            return $"{base.ToString()} | Convertible: {convertibleText} | Trunk Size: {TrunkSize:n1} cu ft";
        }

        /// <summary>
        /// Compares cars for duplicate prevention.
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is CarModel car
                && base.Equals(car)
                && IsConvertible == car.IsConvertible
                && TrunkSize == car.TrunkSize;
        }

        /// <summary>
        /// Returns a hash code for this car.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), IsConvertible, TrunkSize);
        }
    }
}
