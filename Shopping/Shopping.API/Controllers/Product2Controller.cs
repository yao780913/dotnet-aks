using Microsoft.AspNetCore.Mvc;
using Shopping.API.Data;

namespace Shopping.API.Controllers;

[ApiController]
[Route("[controller]")]
public class Product2Controller : ControllerBase
{
    private readonly AppDbContext _db;
    public Product2Controller (AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get ()
    {
        return Ok(_db.Products.ToList());
    }
}