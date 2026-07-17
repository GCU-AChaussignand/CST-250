// Aaron Chaussignand
// CST-250 Activity 1 - xUnit Tests
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, xUnit documentation, and course-provided examples.

using VehicleClassLibrary.Models;
using Xunit;
using VehicleClassLibrary.Services.BusinessLogicLayer;

namespace VehicleClassLibrary.Tests
{
    public class StoreLogicTests
    {
        [Fact]
        public void AddVehicleToInventory_ShouldIncreaseInventoryCount()
        {
            StoreLogic store = new StoreLogic();
            CarModel car = new CarModel
            {
                Id = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                Price = 25000m,
                NumWheels = 4,
                Color = "Blue",
                Mileage = 15000,
                IsConvertible = false,
                TrunkSize = 2.5m
            };

            store.AddVehicleToInventory(car);
            List<VehicleModel> inventory = store.GetInventory();

            Assert.Contains(car, inventory);
            Assert.Single(inventory);
        }

        [Fact]
        public void GetInventory_ShouldReturnEmptyList_WhenNoVehiclesAdded()
        {
            StoreLogic store = new StoreLogic();
            List<VehicleModel> inventory = store.GetInventory();

            Assert.Empty(inventory);
        }

        [Fact]
        public void AddVehicleToCart_ShouldAddVehicle_WhenValidVehicleIdGiven()
        {
            StoreLogic store = new StoreLogic();
            CarModel car = new CarModel
            {
                Id = 1,
                Make = "Honda",
                Model = "Civic",
                Year = 2019,
                Price = 20000m,
                NumWheels = 4,
                Color = "White",
                Mileage = 25000,
                IsConvertible = false,
                TrunkSize = 2.5m
            };

            store.AddVehicleToInventory(car);
            int result = store.AddVehicleToCart(car.Id);
            List<VehicleModel> cart = store.GetShoppingCart();

            Assert.Equal(1, result);
            Assert.Contains(cart, verify => verify.Id == car.Id);
        }

        [Fact]
        public void GetShoppingCart_ShouldReturnEmptyList_WhenNoVehiclesAdded()
        {
            StoreLogic store = new StoreLogic();
            List<VehicleModel> cart = store.GetShoppingCart();

            Assert.Empty(cart);
        }

        [Fact]
        public void Checkout_ShouldReturnCorrectTotal_AndClearCart()
        {
            StoreLogic store = new StoreLogic();
            CarModel car1 = new CarModel
            {
                Id = 3,
                Make = "Ford",
                Model = "F-150",
                Year = 2021,
                Price = 40000m,
                NumWheels = 4,
                Color = "Black",
                Mileage = 12000,
                IsConvertible = false,
                TrunkSize = 2.5m
            };
            PickupModel pickup = new PickupModel
            {
                Id = 4,
                Make = "Chevrolet",
                Model = "Silverado",
                Year = 2022,
                Price = 45000m,
                NumWheels = 4,
                Color = "Red",
                Mileage = 9000,
                HasBedCover = true,
                BedSize = 6.5m
            };

            store.AddVehicleToInventory(car1);
            store.AddVehicleToInventory(pickup);
            store.AddVehicleToCart(car1.Id);
            store.AddVehicleToCart(pickup.Id);
            decimal total = store.Checkout();
            List<VehicleModel> cartAfterCheckout = store.GetShoppingCart();

            Assert.Equal(car1.Price + pickup.Price, total);
            Assert.Empty(cartAfterCheckout);
        }

        [Fact]
        public void AddVehicleToCart_ShouldReturnNegativeOne_WhenInvalidIdGiven()
        {
            StoreLogic store = new StoreLogic();
            int result = store.AddVehicleToCart(99);

            Assert.Equal(-1, result);
            Assert.Empty(store.GetShoppingCart());
        }

        [Fact]
        public void SearchInventory_ShouldFindVehicle_ByMakeModelYearOrColor()
        {
            StoreLogic store = new StoreLogic();
            store.AddVehicleToInventory(new VehicleModel(0, "Subaru", "Outback", 2024, 35000m, 4, "Green", 1000));
            store.AddVehicleToInventory(new VehicleModel(0, "Ford", "Bronco", 2022, 42000m, 4, "Blue", 15000));

            List<VehicleModel> searchResults = store.SearchInventory("subaru");

            Assert.Single(searchResults);
            Assert.Equal("Outback", searchResults[0].Model);
        }

        [Fact]
        public void RemoveVehicleFromCart_ShouldRemoveSelectedVehicle_WhenVehicleExists()
        {
            StoreLogic store = new StoreLogic();
            VehicleModel vehicle = new VehicleModel(0, "Mazda", "CX-5", 2023, 30000m, 4, "Gray", 8000);

            store.AddVehicleToInventory(vehicle);
            store.AddVehicleToCart(vehicle.Id);
            bool removed = store.RemoveVehicleFromCart(vehicle.Id);

            Assert.True(removed);
            Assert.Empty(store.GetShoppingCart());
        }
    }
}
