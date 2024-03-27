using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Commands;

public class ProductCommand: BaseProductRepository
{

    public ProductCommand(NorthwindDbContext db, IMapper mapper): base(db, mapper)
    {
    }

    internal ProductDto CreateUpdateProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);

        if (productDto.ProductId is not null)
        {
            _db.Products.Update(product);
        }
        else
        {
            _db.Products.Add(product);
        }

        int affected = _db.SaveChanges();
        return _mapper.Map<Product, ProductDto>(product);


    }

    public bool DeleteProduct(Product? product)
    {
        try
        {
            //Product? product = base._productQuery.FindProduct(productId: productId);
            //IQueryable<Product>? products = _db.Products;
            //Product product = _mapper.Map<Product>(productDto);

            if (product is null)
                return false;

            product.IsDeleted = true;
            int affected = _db.SaveChanges();


            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
