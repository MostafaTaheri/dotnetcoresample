using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string CategoryName { get; set; }

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; }
        = new HashSet<Product>();

    public bool IsDeleted { get; set; } = false;
}