using Microsoft.AspNetCore.Mvc;
using Northwind.Reposotories;
using Northwind.EntityModels;

namespace Northwind.Controllers;

[ApiController]
[Route("api/v1/product/")]
public class ProductController : ControllerBase
{
    protected ResponseDto _response;
    private IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        this._response = new();
    }

    [HttpGet]
    public object Get()
    {
        try
        {
            IEnumerable<ProductDto> productDtos = _productRepository.GetProducts();
            _response.Result = productDtos;
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
            ProductDto productDtos = _productRepository.GetProduct(productId: id);
            _response.Result = productDtos;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost]
    public object Post([FromBody] ProductDto productDto)
    {
        try
        {
            ProductDto result = _productRepository.CreateUpdateProduct(productDto: productDto);
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
    public object Put([FromBody] ProductDto productDto, int id)
    {
        try
        {
            productDto.ProductId = id;
            ProductDto result = _productRepository.CreateUpdateProduct(productDto: productDto);
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
            _response.IsSuccess = _productRepository.DeleteProduct(productId: id);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.ToString() };
        }

        return _response;
    }

}

