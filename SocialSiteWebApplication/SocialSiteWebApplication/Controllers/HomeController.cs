using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialSiteWebApplication.Models;

namespace SocialSiteWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult RedirectToUser(string userName)
    {
        return userName == "Admin"
            ? RedirectToAction("Index", "Admin", new { userName })
            : RedirectToAction("Index", "User", new { userName });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message)
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message});
    }
}