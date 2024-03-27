using Northwind.EntityModels;

namespace Northwind.Reposotories;


public interface ICategoryRepository
{
    IEnumerable<CategoryDto> GetCategories();
    CategoryDto GetCategory(int categoryId);
    CategoryDto CreateUpdateCategory(CategoryDto categoryDto);
    bool DeleteCategory(int categoryId);
}