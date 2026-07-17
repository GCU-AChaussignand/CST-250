// Aaron Chaussignand
// CST-250 Activity 1 - VehicleStoreConsoleApp
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, and Microsoft C# console documentation.

using VehicleClassLibrary.Models;
using VehicleClassLibrary.Services.BusinessLogicLayer;

Console.WriteLine("Welcome to the Vehicle Store Console Application");
Console.WriteLine("This console app uses the same class library as the GUI application.");
Console.WriteLine();
ControlLoop();

// Reads the user's menu choice and validates integer input.
static int ReadChoice()
{
    int choice = -1;

    while (choice == -1)
    {
        Console.WriteLine();
        Console.WriteLine("Vehicle Store Menu");
        Console.WriteLine("1. Print inventory");
        Console.WriteLine("2. Print shopping cart");
        Console.WriteLine("3. Create a vehicle");
        Console.WriteLine("4. Add vehicle to cart");
        Console.WriteLine("5. Checkout");
        Console.WriteLine("6. Save inventory to file");
        Console.WriteLine("7. Load inventory from file");
        Console.WriteLine("8. Search inventory");
        Console.WriteLine("9. Sort inventory");
        Console.WriteLine("0. Exit");
        Console.Write("Enter choice: ");

        string? input = Console.ReadLine();

        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new FormatException();
            }

            choice = int.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid whole number.");
            choice = -1;
        }
        catch (OverflowException)
        {
            Console.WriteLine("The number entered is too large.");
            choice = -1;
        }
    }

    return choice;
}

// Controls the main menu loop for the console store.
static void ControlLoop()
{
    StoreLogic storeLogic = new StoreLogic();
    int choice = -1;

    while (choice != 0)
    {
        choice = ReadChoice();

        switch (choice)
        {
            case 0:
                Console.WriteLine("Thank you for using the Vehicle Store.");
                break;
            case 1:
                PrintVehicles("Inventory", storeLogic.GetInventory());
                break;
            case 2:
                PrintVehicles("Shopping Cart", storeLogic.GetShoppingCart());
                break;
            case 3:
                CreateVehicle(storeLogic);
                break;
            case 4:
                AddVehicleToCart(storeLogic);
                break;
            case 5:
                decimal total = storeLogic.Checkout();
                Console.WriteLine($"Checkout complete. Total: {total:C2}");
                break;
            case 6:
                bool writeSuccess = storeLogic.WriteInventory();
                Console.WriteLine(writeSuccess ? "Inventory saved." : "Inventory could not be saved.");
                break;
            case 7:
                storeLogic.ReadInventory();
                Console.WriteLine("Inventory load attempted. Current inventory:");
                PrintVehicles("Inventory", storeLogic.GetInventory());
                break;
            case 8:
                Console.Write("Enter make, model, year, or color to search: ");
                string searchText = Console.ReadLine() ?? string.Empty;
                PrintVehicles("Search Results", storeLogic.SearchInventory(searchText));
                break;
            case 9:
                Console.Write("Sort by make, model, year, or price: ");
                string sortBy = Console.ReadLine() ?? string.Empty;
                storeLogic.SortInventory(sortBy);
                PrintVehicles("Sorted Inventory", storeLogic.GetInventory());
                break;
            default:
                Console.WriteLine("Invalid menu choice. Please try again.");
                break;
        }
    }
} // End ControlLoop

// Prints a list of vehicles to the console.
static void PrintVehicles(string title, IEnumerable<VehicleModel> vehicles)
{
    Console.WriteLine();
    Console.WriteLine($"--- {title} ---");

    if (!vehicles.Any())
    {
        Console.WriteLine("No vehicles to display.");
        return;
    }

    foreach (VehicleModel vehicle in vehicles)
    {
        Console.WriteLine(vehicle);
    }
}

// Creates a new vehicle based on user input.
static void CreateVehicle(StoreLogic storeLogic)
{
    Console.WriteLine("Select vehicle type:");
    Console.WriteLine("1. Car");
    Console.WriteLine("2. Motorcycle");
    Console.WriteLine("3. Pickup");
    Console.WriteLine("4. General Vehicle");

    int vehicleType = ReadInteger("Vehicle type: ", 1, 4);
    string make = ReadRequiredString("Make: ");
    string model = ReadRequiredString("Model: ");
    int year = ReadInteger("Year: ", 1886, DateTime.Now.Year + 1);
    decimal price = ReadDecimal("Price: ", 0, decimal.MaxValue);
    int numWheels = ReadInteger("Number of wheels: ", 1, 18);
    string color = ReadRequiredString("Color: ");
    int mileage = ReadInteger("Mileage: ", 0, int.MaxValue);

    VehicleModel vehicle;

    switch (vehicleType)
    {
        case 1:
            bool isConvertible = ReadBoolean("Is convertible? (y/n): ");
            decimal trunkSize = ReadDecimal("Trunk size: ", 0, decimal.MaxValue);
            vehicle = new CarModel(0, make, model, year, price, numWheels, color, mileage, isConvertible, trunkSize);
            break;
        case 2:
            bool hasSideCar = ReadBoolean("Has side car? (y/n): ");
            decimal seatHeight = ReadDecimal("Seat height: ", 0, decimal.MaxValue);
            vehicle = new MotorcycleModel(0, make, model, year, price, numWheels, color, mileage, hasSideCar, seatHeight);
            break;
        case 3:
            bool hasBedCover = ReadBoolean("Has bed cover? (y/n): ");
            decimal bedSize = ReadDecimal("Bed size: ", 0, decimal.MaxValue);
            vehicle = new PickupModel(0, make, model, year, price, numWheels, color, mileage, hasBedCover, bedSize);
            break;
        default:
            vehicle = new VehicleModel(0, make, model, year, price, numWheels, color, mileage);
            break;
    }

    int result = storeLogic.AddVehicleToInventory(vehicle);

    if (result == 0)
    {
        Console.WriteLine("Duplicate vehicle was not added.");
    }
    else if (result > 0)
    {
        Console.WriteLine($"Vehicle added with ID {result}: {vehicle}");
    }
    else
    {
        Console.WriteLine("Vehicle could not be added.");
    }
}

// Adds a vehicle to the shopping cart by id.
static void AddVehicleToCart(StoreLogic storeLogic)
{
    int vehicleId = ReadInteger("Enter vehicle ID to add to cart: ", 1, int.MaxValue);
    int cartCount = storeLogic.AddVehicleToCart(vehicleId);

    if (cartCount == -1)
    {
        Console.WriteLine("No vehicle was found with that ID.");
    }
    else
    {
        Console.WriteLine($"Vehicle added to cart. Cart count: {cartCount}");
    }
}

// Reads a required string from the user.
static string ReadRequiredString(string prompt)
{
    string? input = string.Empty;

    while (string.IsNullOrWhiteSpace(input))
    {
        Console.Write(prompt);
        input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("This field is required.");
        }
    }

    return input.Trim();
}

// Reads a valid integer within a range from the user.
static int ReadInteger(string prompt, int min, int max)
{
    int value;
    bool isValid = false;

    do
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        isValid = int.TryParse(input, out value) && value >= min && value <= max;

        if (!isValid)
        {
            Console.WriteLine($"Please enter a whole number from {min} to {max}.");
        }
    } while (!isValid);

    return value;
}

// Reads a valid decimal within a range from the user.
static decimal ReadDecimal(string prompt, decimal min, decimal max)
{
    decimal value;
    bool isValid = false;

    do
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        isValid = decimal.TryParse(input, out value) && value >= min && value <= max;

        if (!isValid)
        {
            Console.WriteLine($"Please enter a decimal value from {min} to {max}.");
        }
    } while (!isValid);

    return value;
}

// Reads a yes or no Boolean value from the user.
static bool ReadBoolean(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

        if (input == "y" || input == "yes" || input == "true")
        {
            return true;
        }

        if (input == "n" || input == "no" || input == "false")
        {
            return false;
        }

        Console.WriteLine("Please enter y or n.");
    }
}
