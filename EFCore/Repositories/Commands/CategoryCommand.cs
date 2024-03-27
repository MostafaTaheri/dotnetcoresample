using AutoMapper;
using Northwind.EntityModels;

namespace Northwind.Reposotories.Commands;

public class CategoryCommand: BaseCategoryRepository
{

    public CategoryCommand(NorthwindDbContext db, IMapper mapper): base(db, mapper)
    {
    }

    public CategoryDto CreateUpdateCategory(CategoryDto categoryDto)
    {
        Category category = _mapper.Map<Category>(categoryDto);

        if (categoryDto.CategoryId is not null)
        {
            _db.Categories.Update(category);
        }
        else
        {
            _db.Categories.Add(category);
        }

        int affected = _db.SaveChanges();
        return _mapper.Map<Category, CategoryDto>(category);


    }

    public bool DeleteCategory(Category? category)
    {
        try
        {
            //Category? category = _categoryQuery.FindCategory(categoryId: categoryId);
            //IQueryable<Category>? categories = _db.Categories;

            if (category is null)
                return false;

            category.IsDeleted = true;
            _db.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
