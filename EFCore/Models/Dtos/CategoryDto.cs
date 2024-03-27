namespace Northwind.EntityModels;

public class CategoryDto
{
    public int? CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; } = false;
}