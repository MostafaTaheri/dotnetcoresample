using AutoMapper;
using Northwind.EntityModels;
using Northwind.Reposotories.Queries;
using Northwind.Reposotories.Commands;

namespace Northwind.Reposotories;

public abstract class BaseCategoryRepository
{
    internal readonly NorthwindDbContext _db;
    internal readonly IMapper _mapper;

    internal CategoryCommand _categroyCommand;
    internal CategoryQuery _categoryQuery;

    public BaseCategoryRepository(NorthwindDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
