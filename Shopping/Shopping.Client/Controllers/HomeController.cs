using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shopping.Client.Models;
using Shopping.Domain;

namespace Shopping.Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    public HomeController (ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        
        _httpClient = httpClientFactory.CreateClient("ShoppingAPIClient");
    }

    public async Task<IActionResult> Index ()
    {
        var response = await _httpClient.GetAsync("/product");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(products);
    }

    public IActionResult Privacy ()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error ()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}