using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Queries;

public partial class CategoryQuery: BaseCategoryRepository
{

    public CategoryQuery(NorthwindDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    internal Category? FindCategory(int categoryId)
    {
        return _db.Categories?.FirstOrDefault(entity => entity.CategoryId == categoryId);
    }

    public IEnumerable<CategoryDto> GetCategories()
    {
        //IQueryable<Category> _categories = _db.Categories
        //    .Include(c => c.Products.Where(p => p.Stock >= 1));

        IEnumerable<Category>? categories = _db.Categories.ToList();
        return _mapper.Map<List<CategoryDto>>(categories);

    }

    public CategoryDto GetCategory(int categoryId)
    {
        return _mapper.Map<CategoryDto>(FindCategory(categoryId: categoryId));
    }
}
