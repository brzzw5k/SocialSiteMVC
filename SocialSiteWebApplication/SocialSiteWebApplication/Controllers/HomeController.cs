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
    public IActionResult RedirectToUser()
    {
        Request.Form.TryGetValue("username", out var username);
        return username != "Admin" ? 
            RedirectToAction("Index", "User", new {userName = username}):
            RedirectToAction("Index", "Admin", new {userName = username});
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}