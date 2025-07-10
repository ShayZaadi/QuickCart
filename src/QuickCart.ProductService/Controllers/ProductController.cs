using Microsoft.AspNetCore.Mvc;
using QuickCart.ProductService.Contracts;
using QuickCart.ProductService.Models;

namespace QuickCart.ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "AirPods Pro", Description = "Wireless earbuds", Price = 899, Available = true },
        new Product { Id = 2, Name = "MacBook Air", Description = "13-inch M2 chip", Price = 5499, Available = true }
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> Get()
        => Ok(_products.Select(p => new ProductDto(p.Id, p.Name, p.Description ?? "", p.Price, p.Available)));

    [HttpGet("{id}")]
    public ActionResult<ProductDto> Get(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return NotFound();

        return Ok(new ProductDto(product.Id, product.Name, product.Description ?? "", product.Price, product.Available));
    }

    [HttpPost]
    public ActionResult Create(ProductDto dto)
    {
        var product = new Product
        {
            Id = _products.Count + 1,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Available = dto.Available
        };
        _products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, ProductDto dto)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return NotFound();

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Available = dto.Available;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return NotFound();

        _products.Remove(product);
        return NoContent();
    }
}
