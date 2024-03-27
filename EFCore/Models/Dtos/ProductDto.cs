namespace Northwind.EntityModels;

public class ProductDto
{
    public int? ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal? Cost { get; set; }

    public short? Stock { get; set; }

    public bool Discontinued { get; set; }

    public int CategoryId { get; set; }

    public bool IsDeleted { get; set; } = false;


}