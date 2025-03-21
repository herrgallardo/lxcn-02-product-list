// Start with a clear console
Console.Clear();
// Initialize the product manager
ProductManager productManager = new ProductManager();
bool exitApplication = false;

Console.WriteLine("*** PRODUCT LIST APPLICATION ***");

// Main application loop
while (!exitApplication)
{
    DisplayMainMenu();
    // Read user choice and convert to lowercase, handling potential null values
    string choice = Console.ReadLine()?.ToLower() ?? "";

    switch (choice)
    {
        case "1":
            AddProducts(productManager);
            break;
        case "2":
            Console.Clear();
            productManager.DisplayProducts();
            PressAnyKeyToContinue();
            break;
        case "q":
            exitApplication = true;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            PressAnyKeyToContinue();
            break;
    }
}

Console.WriteLine("Thank you for using the Product List Application!");

// Helper methods for the main program
void DisplayMainMenu()
{
    Console.Clear();
    Console.WriteLine("*** PRODUCT LIST APPLICATION ***");
    Console.WriteLine("1. Add products");
    Console.WriteLine("2. View products");
    Console.WriteLine("q. Quit application");
    Console.Write("Select an option: ");
}

void AddProducts(ProductManager manager)
{
    Console.Clear();
    Console.WriteLine("*** ADD PRODUCTS ***");
    Console.WriteLine("(Enter 'q' to return to main menu)");

    string input = "";
    while (input.ToLower() != "q")
    {
        string category = "";
        string productName = "";
        decimal price = 0;
        bool isValidProduct = false;

        Console.WriteLine("");
        Console.WriteLine("Enter Product details (or 'q' to return to main menu)");

        // Get and validate category
        while (!isValidProduct && input.ToLower() != "q")
        {
            try
            {
                // Get category input and handle potential null values
                Console.Write("Category: ");
                input = Console.ReadLine() ?? ""; // Use empty string if input is null
                if (input.ToLower() == "q") break;

                // Validate category length to maintain display format
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Category cannot be empty.");
                if (input.Length > 25)
                    throw new ArgumentException("Category name is too long. Maximum 25 characters allowed.");

                category = input;
                isValidProduct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isValidProduct = false;
            }
        }

        if (input.ToLower() == "q") break;
        isValidProduct = false;

        // Get and validate product name
        while (!isValidProduct && input.ToLower() != "q")
        {
            try
            {
                // Get product name and handle potential null values
                Console.Write("Product Name: ");
                input = Console.ReadLine() ?? ""; // Use empty string if input is null
                if (input.ToLower() == "q") break;

                // Validate product name length to maintain display format
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Product name cannot be empty.");
                if (input.Length > 25)
                    throw new ArgumentException("Product name is too long. Maximum 25 characters allowed.");

                productName = input;
                isValidProduct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isValidProduct = false;
            }
        }

        if (input.ToLower() == "q") break;
        isValidProduct = false;

        // Get and validate price
        while (!isValidProduct && input.ToLower() != "q")
        {
            try
            {
                // Get and validate price input
                Console.Write("Price: ");
                input = Console.ReadLine() ?? ""; // Use empty string if input is null
                if (input.ToLower() == "q") break;

                // Convert string to decimal and validate format
                if (!decimal.TryParse(input, out price))
                    throw new FormatException("Invalid price format. Please enter a valid number.");

                // Validate price range
                if (price < 0)
                    throw new ArgumentException("Price cannot be negative.");
                if (price > 1000000)
                    throw new ArgumentException("Price is too high. Maximum value is 1,000,000.");

                isValidProduct = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                isValidProduct = false;
            }
        }

        if (input.ToLower() == "q") break;

        // Add the product if all inputs are valid
        try
        {
            manager.AddProduct(category, productName, price);
            Console.WriteLine($"Product '{productName}' added successfully!");
            input = ""; // Reset input to continue adding products
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product: {ex.Message}");
        }
    }
}

void PressAnyKeyToContinue()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

// Represents a product with category, name, and price
class Product
{
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string category, string name, decimal price)
    {
        // Validate inputs before setting properties
        // This second layer of validation ensures data integrity even if UI validation is bypassed

        // Category validation
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty.");
        if (category.Length > 25)
            throw new ArgumentException("Category name is too long. Maximum 25 characters allowed.");

        // Product name validation
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.");
        if (name.Length > 25)
            throw new ArgumentException("Product name is too long. Maximum 25 characters allowed.");

        // Price validation - ensures business rules are enforced
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");
        if (price > 1000000)
            throw new ArgumentException("Price is too high. Maximum value is 1,000,000.");

        Category = category;
        Name = name;
        Price = price;
    }
}

// Manages a collection of products with operations for adding and displaying
class ProductManager
{
    private List<Product> _products;

    public ProductManager()
    {
        _products = new List<Product>();
    }

    // Adds a new product to the collection
    public void AddProduct(string category, string name, decimal price)
    {
        Product product = new Product(category, name, price);
        _products.Add(product);
    }

    // Gets the total price of all products - refactored with LINQ
    public decimal GetTotalPrice() => _products.Sum(p => p.Price);

    // Displays all products sorted by price from low to high
    public void DisplayProducts()
    {
        Console.WriteLine("\n=================================================================");
        Console.WriteLine(" PRODUCTS IN YOUR LIST ");
        Console.WriteLine("=================================================================");

        if (!_products.Any()) // LINQ to check if list is empty
        {
            Console.WriteLine("No products added.");
        }
        else
        {
            // Sort products by price from low to high using LINQ
            var sortedProducts = _products.OrderBy(p => p.Price);

            // Print header
            Console.WriteLine($"{"Category",-25} {"Product",-25} {"Price",10}");
            Console.WriteLine(new string('-', 62));

            // Print each product with formatting
            foreach (var product in sortedProducts)
            {
                Console.WriteLine($"{product.Category,-25} {product.Name,-25} {product.Price,10:F2}");
            }

            // Print summary
            Console.WriteLine(new string('-', 62));
            Console.WriteLine($"{"Total",-51} {GetTotalPrice(),10:F2}");
        }

        Console.WriteLine("=================================================================");
    }
}