using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webservice.Entities;
using webservice.Models;

namespace webservice.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _client;

    public HomeController()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_SERVICE") ?? string.Empty);
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    [HttpGet("usersList")]
    public IActionResult Index()
    {
        var response = _client.GetAsync("users").Result.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<List<User>>(response);

        if (result != null)
        {
            return View(result);
        }

        return BadRequest("Users is null!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}