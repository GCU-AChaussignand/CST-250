// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and course-provided examples.

namespace VehicleClassLibrary.Models
{
    /// <summary>
    /// Specialized vehicle model for motorcycles.
    /// </summary>
    public class MotorcycleModel : VehicleModel
    {
        public bool HasSideCar { get; set; }
        public decimal SeatHeight { get; set; }

        /// <summary>
        /// Creates a default motorcycle.
        /// </summary>
        public MotorcycleModel()
            : base()
        {
            HasSideCar = false;
            SeatHeight = 0.0m;
        }

        /// <summary>
        /// Creates a motorcycle with original common properties and motorcycle-specific properties.
        /// </summary>
        public MotorcycleModel(int id, string make, string model, int year, decimal price, int numWheels, bool hasSideCar, decimal seatHeight)
            : base(id, make, model, year, price, numWheels)
        {
            HasSideCar = hasSideCar;
            SeatHeight = seatHeight;
        }

        /// <summary>
        /// Creates a motorcycle with all common properties and motorcycle-specific properties.
        /// </summary>
        public MotorcycleModel(int id, string make, string model, int year, decimal price, int numWheels, string color, int mileage, bool hasSideCar, decimal seatHeight)
            : base(id, make, model, year, price, numWheels, color, mileage)
        {
            HasSideCar = hasSideCar;
            SeatHeight = seatHeight;
        }

        /// <summary>
        /// Formats a motorcycle for display.
        /// </summary>
        public override string ToString()
        {
            string sideCarText = HasSideCar ? "Yes" : "No";
            return $"{base.ToString()} | Side Car: {sideCarText} | Seat Height: {SeatHeight:n1} in";
        }

        /// <summary>
        /// Compares motorcycles for duplicate prevention.
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is MotorcycleModel motorcycle
                && base.Equals(motorcycle)
                && HasSideCar == motorcycle.HasSideCar
                && SeatHeight == motorcycle.SeatHeight;
        }

        /// <summary>
        /// Returns a hash code for this motorcycle.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), HasSideCar, SeatHeight);
        }
    }
}
