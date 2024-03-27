using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.EntityModels;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string ProductName { get; set; }

    [Column("UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; }

    [Column("UnitsInStock")]
    public short? Stock { get; set; }

    public bool Discontinued { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;


}