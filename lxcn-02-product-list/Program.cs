// Clear the console at startup
Console.Clear();

Console.WriteLine("*** PRODUCT LIST APPLICATION ***");
Console.WriteLine("");
Console.WriteLine("Add products to your list.");
Console.WriteLine("(Enter 'q' to quit)");

List<Product> products = new List<Product>();
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
            Product product = new Product(category, productName, price);
            products.Add(product);
            Console.WriteLine($"Product '{productName}' added successfully!");
            break;
        }
    }
}

// Clear the console before showing results
Console.Clear();

Console.WriteLine("\n=======================================");
Console.WriteLine("         PRODUCTS IN YOUR LIST         ");
Console.WriteLine("=======================================");

if (products.Count == 0)
{
    Console.WriteLine("No products added.");
}
else
{
    // Print header
    Console.WriteLine($"{"Category",-15} {"Product",-25} {"Price",10}");
    Console.WriteLine(new string('-', 52));
    
    // Print each product with formatting
    foreach (var product in products)
    {
        Console.WriteLine($"{product.Category,-15} {product.Name,-25} {product.Price,10:F2}");
    }
}

Console.WriteLine("=======================================");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

// Product class to store product information
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
    
    public override string ToString()
    {
        return $"{Category,-15} {Name,-25} {Price,10:F2}";
    }
}