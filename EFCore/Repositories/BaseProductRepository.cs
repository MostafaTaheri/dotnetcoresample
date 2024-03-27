using AutoMapper;
using Northwind.EntityModels;
using Northwind.Reposotories.Queries;
using Northwind.Reposotories.Commands;

namespace Northwind.Reposotories;

public abstract class BaseProductRepository
{
    internal readonly NorthwindDbContext _db;
    internal readonly IMapper _mapper;

    internal ProductCommand _productCommand;
    internal ProductQuery _productQuery;

    public BaseProductRepository(NorthwindDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
