using ThreeTierApp.Data;
using ThreeTierApp.BusinessLogic.Models;
using ThreeTierApp.BusinessLogic.Repositories;
using ThreeTierApp.BusinessLogic.Services;

namespace ThreeTierApp.Data.Repositories;

public class ProductRepository : IProductRepository
{
    // I en riktig applikation skulle vi ansluta till en databas
    // men för detta exempel använder vi en in-memory lista
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 12999.99m, Description = "Kraftfull bärbar dator" },
        new Product { Id = 2, Name = "Smartphone", Price = 8999.99m, Description = "Senaste modellen" },
        new Product { Id = 3, Name = "Hörlurar", Price = 1499.99m, Description = "Trådlösa över-örat hörlurar" }
    };

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        // Tilldela ett nytt ID (i en databas skulle detta hanteras automatiskt)
        var maxId = _products.Any() ? _products.Max(p => p.Id) : 0;
        product.Id = maxId + 1;

        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            var index = _products.IndexOf(existingProduct);
            _products[index] = product;
        }
        else
        {
            throw new KeyNotFoundException($"Produkt med ID {product.Id} hittades inte.");
        }
    }

    public void Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
        else
        {
            throw new KeyNotFoundException($"Produkt med ID {id} hittades inte.");
        }
    }
}