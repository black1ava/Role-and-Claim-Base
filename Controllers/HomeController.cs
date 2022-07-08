using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityAppFirstAttempt.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdentityAppFirstAttempt.Controllers;
[Authorize(Policy = "Student")]
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        return Json(new { Name = name });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
