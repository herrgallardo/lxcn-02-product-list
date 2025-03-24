// Start with a clear console
Console.Clear();
// Initialize the product manager
ProductManager productManager = new ProductManager();
bool exitApplication = false;

Console.Title = "Product List Application";

// Display welcome message
DisplayWelcomeScreen();

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

            // Process user input after displaying products
            Console.Write("\nEnter your choice: ");
            string productViewChoice = Console.ReadLine()?.ToLower() ?? "";

            switch (productViewChoice)
            {
                case "p":
                    AddProducts(productManager);
                    break;
                case "s":
                    SearchProducts(productManager);
                    break;
                case "q":
                    break;
                default:
                    DisplayErrorMessage("Invalid option.");
                    PressAnyKeyToContinue();
                    break;
            }
            break;
        case "3":
            SearchProducts(productManager);
            break;
        case "q":
            DisplayExitScreen();
            exitApplication = true;
            break;
        default:
            DisplayErrorMessage("Invalid option. Please try again.");
            PressAnyKeyToContinue();
            break;
    }
}

// Helper methods for the main program
void DisplayWelcomeScreen()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║            PRODUCT LIST APPLICATION                    ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine("\nWelcome to the Product List Application!");
    Console.WriteLine("This application helps you manage and search your product inventory.");
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

void DisplayMainMenu()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║            PRODUCT LIST APPLICATION                    ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine("\nMAIN MENU:");
    Console.WriteLine("--------------------------------------------------------");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("1. Add products");
    Console.WriteLine("2. View products");
    Console.WriteLine("3. Search products");
    Console.WriteLine("q. Quit application");
    Console.ResetColor();

    Console.WriteLine("--------------------------------------------------------");
    Console.Write("Select an option: ");
}

void SearchProducts(ProductManager manager)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                 SEARCH PRODUCTS                        ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    // Check if there are any products to search
    if (manager.ProductCount == 0)
    {
        DisplayWarningMessage("No products to search. Please add products first.");
        PressAnyKeyToContinue();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("\nEnter search term (category or product name): ");
    Console.ResetColor();
    string searchTerm = Console.ReadLine() ?? "";

    // Perform search
    var searchResults = manager.SearchProducts(searchTerm);

    if (searchResults.Any())
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nFound {searchResults.Count} product(s) matching '{searchTerm}'");
        Console.ResetColor();
        // Show all products but highlight matching ones
        manager.DisplayProducts(null, searchTerm);

        // Process user input after displaying search results
        Console.Write("\nEnter your choice: ");
        string searchViewChoice = Console.ReadLine()?.ToLower() ?? "";

        switch (searchViewChoice)
        {
            case "p":
                AddProducts(manager);
                break;
            case "s":
                SearchProducts(manager);
                break;
            case "q":
                // Just return to main menu
                return;
            default:
                DisplayErrorMessage("Invalid option.");
                PressAnyKeyToContinue();
                break;
        }
    }
    else
    {
        DisplayWarningMessage($"No products found matching '{searchTerm}'");
        PressAnyKeyToContinue();
    }
}

void AddProducts(ProductManager manager)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                   ADD PRODUCTS                         ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nTo enter a new product - follow the steps | To quit - enter: \"q\"");
    Console.ResetColor();

    string input = "";
    while (input.ToLower() != "q")
    {
        string category = "";
        string productName = "";
        decimal price = 0;
        bool isValidProduct = false;

        Console.WriteLine("\n--------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Enter Product details (or 'q' to return to main menu)");
        Console.ResetColor();

        // Get and validate category
        while (!isValidProduct && input.ToLower() != "q")
        {
            try
            {
                // Get category input and handle potential null values
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Category: ");
                Console.ResetColor();
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
                DisplayErrorMessage($"Error: {ex.Message}");
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Product Name: ");
                Console.ResetColor();
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
                DisplayErrorMessage($"Error: {ex.Message}");
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Price: ");
                Console.ResetColor();
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
                DisplayErrorMessage($"Error: {ex.Message}");
                isValidProduct = false;
            }
        }

        if (input.ToLower() == "q") break;

        // Add the product if all inputs are valid
        try
        {
            manager.AddProduct(category, productName, price);
            DisplaySuccessMessage($"Product '{productName}' added successfully!");
            Console.WriteLine("--------------------------------------------------------");
            input = ""; // Reset input to continue adding products
        }
        catch (Exception ex)
        {
            DisplayErrorMessage($"Error adding product: {ex.Message}");
        }
    }
}

void DisplaySuccessMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
}

void DisplayErrorMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}

void DisplayWarningMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ResetColor();
}

void DisplayExitScreen()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║            THANK YOU FOR USING THE APP                 ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine("\nThank you for using the Product List Application!");
    Console.WriteLine("Have a great day!\n");
    Thread.Sleep(2000); // Show exit message for 2 seconds
}

void PressAnyKeyToContinue()
{
    Console.WriteLine("\nPress any key to continue...");
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

        // Price validation
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");
        if (price > 1000000)
            throw new ArgumentException("Price is too high. Maximum value is 1,000,000.");

        Category = category;
        Name = name;
        Price = price;
    }
}

// Manages a collection of products with operations for adding, displaying, and searching
class ProductManager
{
    private List<Product> _products;

    public ProductManager()
    {
        _products = new List<Product>();
    }

    // Property to get the number of products
    public int ProductCount => _products.Count;

    // Adds a new product to the collection
    public void AddProduct(string category, string name, decimal price)
    {
        Product product = new Product(category, name, price);
        _products.Add(product);
    }

    // Gets the total price of all products - refactored with LINQ
    public decimal GetTotalPrice() => _products.Sum(p => p.Price);

    // Searches for products by name or category
    public List<Product> SearchProducts(string searchTerm)
    {
        return _products
            .Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Displays all products sorted by price from low to high
    public void DisplayProducts(List<Product>? productsToDisplay = null, string? searchTerm = null)
    {
        Console.Clear(); // Clear the console first
        Console.WriteLine("\n╔═════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     PRODUCTS IN YOUR LIST                            ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════════════════╝");

        // Use the full list of products if no specific list is provided
        var displayList = productsToDisplay ?? _products;

        if (!displayList.Any())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nNo products found.");
            Console.ResetColor();
        }
        else
        {
            // Sort products by price from low to high
            var sortedProducts = displayList.OrderBy(p => p.Price).ToList();

            // Print header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{"Category",-25} {"Product",-25} {"Price",10}");
            Console.WriteLine(new string('─', 62));
            Console.ResetColor();

            // Print each product with formatting and optional highlighting
            foreach (var product in sortedProducts)
            {
                // Check if the product should be highlighted
                bool shouldHighlight = !string.IsNullOrEmpty(searchTerm) &&
                    (product.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                     product.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

                if (shouldHighlight)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    // Alternate colors for easier reading
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"{product.Category,-25} {product.Name,-25} {product.Price,10:N2} kr");

                // Reset colors
                Console.ResetColor();
            }

            // Print summary
            Console.WriteLine(new string('─', 62));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{"Total",-51} {GetTotalPrice(),10:N2} kr");
            Console.ResetColor();
        }

        Console.WriteLine("\n╔═════════════════════════════════════════════════════════════════════╗");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("║  To enter a new product - enter: \"P\" | To search - enter: \"S\"        ║");
        Console.WriteLine("║  To quit - enter: \"Q\"                                                ║");
        Console.ResetColor();
        Console.WriteLine("╚═════════════════════════════════════════════════════════════════════╝");
    }
}