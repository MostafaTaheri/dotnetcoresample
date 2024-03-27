using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Queries;

public partial class ProductQuery: BaseProductRepository
{

    public ProductQuery(NorthwindDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    internal Product? FindProduct(int productId)
    {

        return _db.Products?.FirstOrDefault(entity => entity.ProductId == productId);
    }

    internal IEnumerable<ProductDto> GetProducts()
    {
        IEnumerable<Product>? products = _db.Products.ToList();

        // Try to get an efficient count from EF core DbSet<T>.
        products.TryGetNonEnumeratedCount(out int countList);

        return _mapper.Map<List<ProductDto>>(products);
    }

    internal ProductDto GetProduct(int productId)
    {
        return _mapper.Map<ProductDto>(FindProduct(productId: productId));

    }

    internal IEnumerable<ProductDto> GetProductsByPaging(int pageSize, int currentPage, int totalPage)
    {
        IEnumerable<Product>? products = _db.Products.OrderBy(
            p => p.ProductId).Skip(currentPage * totalPage).Take(pageSize);

        // Try to get an efficient count from EF core DbSet<T>.
        //products.TryGetNonEnumeratedCount(out int countList);

        return _mapper.Map<List<ProductDto>>(products);
    }
}
