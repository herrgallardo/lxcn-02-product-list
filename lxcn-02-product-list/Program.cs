// Clear the console at startup
Console.Clear();

// Initialize the product manager
ProductManager productManager = new ProductManager();

Console.WriteLine("*** PRODUCT LIST APPLICATION ***");
Console.WriteLine("");
Console.WriteLine("Add products to your list.");
Console.WriteLine("(Enter 'q' to quit)");

string input = "";

while (input.ToLower() != "q")
{
    Console.WriteLine("");
    Console.WriteLine("Enter Product details (or 'q' to quit)");

    Console.Write("Category: ");
    input = Console.ReadLine();
    if (input.ToLower() == "q") break;
    string category = input;

    Console.Write("Product Name: ");
    input = Console.ReadLine();
    if (input.ToLower() == "q") break;
    string productName = input;

    Console.Write("Price: ");
    input = Console.ReadLine();
    if (input.ToLower() == "q") break;

    while (true)
    {
        if (!decimal.TryParse(input, out decimal price))
        {
            Console.WriteLine("Invalid price format. Enter the price as a number.");
            Console.Write("Price: ");
            input = Console.ReadLine();

            if (input.ToLower() == "q")
            {
                input = "q";
                break;
            }
        }
        else
        {
            productManager.AddProduct(category, productName, price);
            Console.WriteLine($"Product '{productName}' added successfully!");
            break;
        }
    }
}

// Clear the console before showing results
Console.Clear();

// Display the products sorted by price
productManager.DisplayProducts();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

// Represents a product with category, name, and price
class Product
{
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string category, string name, decimal price)
    {
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

    // Gets the total price of all products
    public decimal GetTotalPrice()
    {
        decimal total = 0;

        foreach (var product in _products)
        {
            total += product.Price;
        }

        return total;
    }

    // Displays all products sorted by price from low to high
    public void DisplayProducts()
    {
        Console.WriteLine("\n=======================================");
        Console.WriteLine("         PRODUCTS IN YOUR LIST         ");
        Console.WriteLine("=======================================");

        if (_products.Count == 0)
        {
            Console.WriteLine("No products added.");
        }
        else
        {
            // Sort products by price from low to high
            List<Product> sortedProducts = _products.OrderBy(p => p.Price).ToList();

            // Print header
            Console.WriteLine($"{"Category",-15} {"Product",-25} {"Price",10}");
            Console.WriteLine(new string('-', 52));

            // Print each product with formatting
            foreach (var product in sortedProducts)
            {
                Console.WriteLine($"{product.Category,-15} {product.Name,-25} {product.Price,10:F2}");
            }

            // Print summary
            Console.WriteLine(new string('-', 52));
            Console.WriteLine($"{"Total",-41} {GetTotalPrice(),10:F2}");
        }

        Console.WriteLine("=======================================");
    }
}