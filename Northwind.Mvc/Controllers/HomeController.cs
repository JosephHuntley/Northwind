using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Northwind.Common;

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory clientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        clientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        // View looks in different paths for the Razor file to generate the web page. The file name must be the same as the method.
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> Customers(string country)
{
  string uri;
  if (string.IsNullOrEmpty(country))
  {
    ViewData["Title"] = "All Customers Worldwide";
    uri = "api/customer";
  }
  else
  {
    ViewData["Title"] = $"Customers in {country}";
    uri = $"api/customer/?country={country}";
  }
  HttpClient client = clientFactory.CreateClient(
    name: "Northwind.WebApi");
  HttpRequestMessage request = new(
    method: HttpMethod.Get, requestUri: uri);
  HttpResponseMessage response = await client.SendAsync(request);
  IEnumerable<Customer>? model = await response.Content
    .ReadFromJsonAsync<IEnumerable<Customer>>();
  return View(model);
}
}
