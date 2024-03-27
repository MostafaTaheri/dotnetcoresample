using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Queries;

public partial class CategoryQuery
{


    internal void JoinCategories()
    {
        var queryJoin = _db.Categories.Join(
            inner: _db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) =>
                new { c.CategoryName, p.ProductName, p.ProductId }
            );
    }


    internal void GroupJoinCategories()
    {
        var queryJoin = _db.Categories.AsEnumerable().GroupJoin(
            inner: _db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, matchingProducts) =>
                new {
                    c.CategoryName,
                    Products = matchingProducts.OrderBy(p => p.ProductName)
                }
            );
    }

    internal void ProductLookup()
    {
        var productQuery = _db.Categories.Join(
            inner: _db.Products,
            outerKeySelector: category => category.CategoryId,
            innerKeySelector: product => product.CategoryId,
            resultSelector: (c, p) =>
                new { c.CategoryName, Product = p }
            );

        ILookup<string, Product> productLookup = productQuery.ToLookup(
            keySelector: cp => cp.CategoryName,
            elementSelector: cp => cp.Product);
    }
}
