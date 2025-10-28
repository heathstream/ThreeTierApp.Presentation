

using ThreeTierApp.BusinessLogic.Services;
using ThreeTierApp.BusinessLogic.Repositories;
using ThreeTierApp.Data.Repositories;
using ThreeTierApp.BusinessLogic.Models;

namespace ThreeTierApp.Presentation;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Three-Tier Application Demo");
        Console.WriteLine("==========================");

        // Skapa en instans av repository-implementationen
        IProductRepository productRepository = new ProductRepository();

        // Skicka in repository till service-konstruktorn
        IProductService productService = new ProductService(productRepository);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nVälj ett alternativ:");
            Console.WriteLine("1. Visa alla produkter");
            Console.WriteLine("2. Visa produkt med ID");
            Console.WriteLine("3. Lägg till produkt");
            Console.WriteLine("4. Uppdatera produkt");
            Console.WriteLine("5. Ta bort produkt");
            Console.WriteLine("0. Avsluta");

            Console.Write("\nDitt val: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        ShowAllProducts(productService);
                        break;
                    case 2:
                        ShowProductById(productService);
                        break;
                    case 3:
                        AddProduct(productService);
                        break;
                    case 4:
                        UpdateProduct(productService);
                        break;
                    case 5:
                        DeleteProduct(productService);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }
        }
    }

    static void ShowAllProducts(IProductService productService)
    {
        Console.WriteLine("\nAlla produkter:");
        var products = productService.GetAllProducts();

        if (!products.Any())
        {
            Console.WriteLine("Inga produkter finns.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Namn: {product.Name}, Pris: {product.Price:C}");
        }
    }

    static void ShowProductById(IProductService productService)
    {
        Console.Write("Ange produkt-ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        var product = productService.GetProductById(id);
        if (product == null)
        {
            Console.WriteLine($"Produkt med ID {id} hittades inte.");
            return;
        }

        Console.WriteLine("\nProduktdetaljer:");
        Console.WriteLine($"ID: {product.Id}");
        Console.WriteLine($"Namn: {product.Name}");
        Console.WriteLine($"Pris: {product.Price:C}");
        Console.WriteLine($"Beskrivning: {product.Description}");
    }

    static void AddProduct(IProductService productService)
    {
        var product = new Product();

        Console.Write("Ange produktnamn: ");
        product.Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Ange pris: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Ogiltigt pris.");
            return;
        }
        product.Price = price;

        Console.Write("Ange beskrivning: ");
        product.Description = Console.ReadLine() ?? string.Empty;

        productService.AddProduct(product);
        Console.WriteLine("Produkt har lagts till.");
    }

    static void UpdateProduct(IProductService productService)
    {
        Console.Write("Ange produkt-ID att uppdatera: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        var existingProduct = productService.GetProductById(id);
        if (existingProduct == null)
        {
            Console.WriteLine($"Produkt med ID {id} hittades inte.");
            return;
        }

        Console.Write($"Ange nytt namn (nuvarande: {existingProduct.Name}): ");
        var name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name))
        {
            existingProduct.Name = name;
        }

        Console.Write($"Ange nytt pris (nuvarande: {existingProduct.Price:C}): ");
        var priceStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(priceStr) && decimal.TryParse(priceStr, out decimal price))
        {
            existingProduct.Price = price;
        }

        Console.Write($"Ange ny beskrivning (nuvarande: {existingProduct.Description}): ");
        var description = Console.ReadLine();
        if (!string.IsNullOrEmpty(description))
        {
            existingProduct.Description = description;
        }

        productService.UpdateProduct(existingProduct);
        Console.WriteLine("Produkt har uppdaterats.");
    }

    static void DeleteProduct(IProductService productService)
    {
        Console.Write("Ange produkt-ID att ta bort: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        var product = productService.GetProductById(id);
        if (product == null)
        {
            Console.WriteLine($"Produkt med ID {id} hittades inte.");
            return;
        }

        Console.Write($"Är du säker på att du vill ta bort {product.Name}? (j/n): ");
        var confirm = Console.ReadLine()?.ToLower();
        if (confirm != "j" && confirm != "ja")
        {
            Console.WriteLine("Borttagning avbruten.");
            return;
        }

        productService.DeleteProduct(id);
        Console.WriteLine("Produkt har tagits bort.");
    }
}