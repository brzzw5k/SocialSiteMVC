using Microsoft.AspNetCore.Mvc;
using SocialSiteWebApplication.Models;

namespace SocialSiteWebApplication.Controllers;

public class AdminController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private DataStore.DataStore _dataStore;
    
    public AdminController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _dataStore = DataStore.DataStore.Instance;
    }
    
    public IActionResult Index()
    {
        var users = _dataStore.GetUsers();
        return View("Index", new AdminViewModel(users));
    }
}