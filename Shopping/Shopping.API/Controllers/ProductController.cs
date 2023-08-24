using Bogus;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Data;
using Shopping.Domain;

namespace Shopping.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult Get ()
    {
        var faker = new Faker<Product>()
            .RuleFor(p => p.Id, f => f.IndexFaker)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
            .RuleFor(p => p.ImageFile, f => f.Image.LoremFlickrUrl(keywords: f.Commerce.Categories(1)[0]));
        
        return Ok(
            faker.Generate(10));
    }
}