using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Northwind.Reposotories;
using Northwind.EntityModels;
using Northwind.Reposotories.Caches;

namespace Northwind.Controllers;

[ApiController]
[Route("api/v1/category/")]
public class CategoryController : ControllerBase
{
    protected ResponseDto _response;
    private ICategoryRepository _categoryRepository;
    private Redis _redis;

    public CategoryController(ICategoryRepository categoryRepository, IDistributedCache cache)
    {

        _categoryRepository = categoryRepository;
        this._response = new();
        _redis = new(cache);
    }

    [HttpGet]
    public object Get()
    {
        try
        {
            IEnumerable<CategoryDto> categoryDtos = _categoryRepository.GetCategories();
            _response.Result = categoryDtos;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

    [HttpGet]
    [Route("{id}")]
    public object Get(int id)
    {
        try
        {
            _redis.Set("Mosatafaa", "MHTasjhdauiq734j32n", 20);
            string s = _redis.Get("Mostafa");
            CategoryDto categoryDto = _categoryRepository.GetCategory(categoryId: id);
            _response.Result = categoryDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost]
    public object Post([FromBody] CategoryDto categoryDto)
    {
        try
        {
            CategoryDto result = _categoryRepository.CreateUpdateCategory(categoryDto: categoryDto);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

    [HttpPut]
    [Route("{id}")]
    public object Put([FromBody] CategoryDto categoryDto, int id)
    {
        try
        {
            categoryDto.CategoryId = id;
            CategoryDto result = _categoryRepository.CreateUpdateCategory(categoryDto: categoryDto);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

    [HttpDelete]
    [Route("{id}")]
    public object Delete(int id)
    {
        try
        {
            _response.IsSuccess = _categoryRepository.DeleteCategory(categoryId: id);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

}

