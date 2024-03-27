using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Queries;

public partial class ProductQuery
{

    //public ProductQuery(NorthwindDbContext db, IMapper mapper) : base(db, mapper)
    //{
    //}

    internal void GetProductByNames(string[] names)
    {
        var query = from name in names where name.EndsWith('B') select name;

        //query = names.Where(name => name.EndsWith('B') && name.Length > 4).
        //    OrderBy(name => name.Length).ThenBy(name => name);

        IOrderedEnumerable<string> query2 = names.Where(
            name => name.EndsWith('B') && name.Length > 4).
            OrderBy(name => name.Length).ThenBy(name => name);

        //return _mapper.Map<ProductDto>(FindProduct(productId: productId));

    }

    internal ProductDto GetProductBySpecificEntity(int productId)
    {
        var products = _db.Products.Select(entity => new
        {
            entity.ProductId,
            entity.ProductName,
            entity.Cost
        }).FirstOrDefault(
            entity => entity.ProductId == productId
            );

        return _mapper.Map<ProductDto>(products);
    }
}
