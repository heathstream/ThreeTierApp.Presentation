
using ThreeTierApp.BusinessLogic.Models;
using ThreeTierApp.BusinessLogic.Repositories;

namespace ThreeTierApp.BusinessLogic.Services;

public class ProductService : IProductService
{


    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }




    public IEnumerable<Product> GetAllProducts()
    {
        // Här kan vi lägga till affärslogik innan vi anropar repository
        return _productRepository.GetAll();
    }

    public Product? GetProductById(int id)
    {
        return _productRepository.GetById(id);
    }

    public void AddProduct(Product product)
    {
        // Validera data innan vi lägger till
        if (string.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentException("Produktnamn får inte vara tomt.");
        }

        if (product.Price < 0)
        {
            throw new ArgumentException("Pris får inte vara negativt.");
        }

        _productRepository.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        // Validera data innan vi uppdaterar
        if (string.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentException("Produktnamn får inte vara tomt.");
        }

        if (product.Price < 0)
        {
            throw new ArgumentException("Pris får inte vara negativt.");
        }

        _productRepository.Update(product);
    }

    public void DeleteProduct(int id)
    {
        _productRepository.Delete(id);
    }
}
