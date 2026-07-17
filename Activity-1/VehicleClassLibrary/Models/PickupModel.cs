// Aaron Chaussignand
// CST-250 Activity 1 - VehicleClassLibrary
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and course-provided examples.

namespace VehicleClassLibrary.Models
{
    /// <summary>
    /// Specialized vehicle model for pickups.
    /// </summary>
    public class PickupModel : VehicleModel
    {
        public bool HasBedCover { get; set; }
        public decimal BedSize { get; set; }

        /// <summary>
        /// Creates a default pickup.
        /// </summary>
        public PickupModel()
            : base()
        {
            HasBedCover = false;
            BedSize = 0.0m;
        }

        /// <summary>
        /// Creates a pickup with original common properties and pickup-specific properties.
        /// </summary>
        public PickupModel(int id, string make, string model, int year, decimal price, int numWheels, bool hasBedCover, decimal bedSize)
            : base(id, make, model, year, price, numWheels)
        {
            HasBedCover = hasBedCover;
            BedSize = bedSize;
        }

        /// <summary>
        /// Creates a pickup with all common properties and pickup-specific properties.
        /// </summary>
        public PickupModel(int id, string make, string model, int year, decimal price, int numWheels, string color, int mileage, bool hasBedCover, decimal bedSize)
            : base(id, make, model, year, price, numWheels, color, mileage)
        {
            HasBedCover = hasBedCover;
            BedSize = bedSize;
        }

        /// <summary>
        /// Formats a pickup for display.
        /// </summary>
        public override string ToString()
        {
            string bedCoverText = HasBedCover ? "Yes" : "No";
            return $"{base.ToString()} | Bed Cover: {bedCoverText} | Bed Size: {BedSize:n1} ft";
        }

        /// <summary>
        /// Compares pickups for duplicate prevention.
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is PickupModel pickup
                && base.Equals(pickup)
                && HasBedCover == pickup.HasBedCover
                && BedSize == pickup.BedSize;
        }

        /// <summary>
        /// Returns a hash code for this pickup.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), HasBedCover, BedSize);
        }
    }
}
