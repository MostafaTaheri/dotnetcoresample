using Northwind.EntityModels;

namespace Northwind.Reposotories;


public interface IProductRepository
{
    IEnumerable<ProductDto> GetProducts();
    ProductDto GetProduct(int productId);
    ProductDto CreateUpdateProduct(ProductDto productDto);
    bool DeleteProduct(int productId);
}