using AutoMapper;

using Northwind.EntityModels;

namespace Northwind.Reposotories;

public class CategoryRepository : BaseCategoryRepository, ICategoryRepository
{

    public CategoryRepository(NorthwindDbContext db, IMapper mapper) : base(db, mapper)
    {
        _categroyCommand = new(db, mapper);
        _categoryQuery = new(db, mapper);
    }

    public CategoryDto CreateUpdateCategory(CategoryDto categoryDto)
    {
        return _categroyCommand.CreateUpdateCategory(categoryDto: categoryDto);
    }

    public bool DeleteCategory(int categoryId)
    {
        return _categroyCommand.DeleteCategory(
            _categoryQuery.FindCategory(categoryId: categoryId));
    }

    public IEnumerable<CategoryDto> GetCategories()
    {
        return _categoryQuery.GetCategories();

    }

    public CategoryDto GetCategory(int categoryId)
    {
        return _categoryQuery.GetCategory(categoryId: categoryId);
    }
}