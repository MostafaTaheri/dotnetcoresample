using AutoMapper;
using Northwind.EntityModels;
using Northwind.Reposotories.Queries;

namespace Northwind.Reposotories;

public class ProductRepository : BaseProductRepository, IProductRepository
{

    public ProductRepository(NorthwindDbContext db, IMapper mapper) : base(db, mapper)
    {
        _productCommand = new(db, mapper);
        _productQuery = new(db, mapper);
    }

    public ProductDto CreateUpdateProduct(ProductDto productDto)
    {
        return _productCommand.CreateUpdateProduct(productDto: productDto);

    }

    public bool DeleteProduct(int productId)
    {
        return _productCommand.DeleteProduct(
            _productQuery.FindProduct(productId: productId));
    }

    public IEnumerable<ProductDto> GetProducts()
    {
        return _productQuery.GetProducts();
    }

    public ProductDto GetProduct(int productId)
    {
        return _productQuery.GetProduct(productId: productId);

    }
}