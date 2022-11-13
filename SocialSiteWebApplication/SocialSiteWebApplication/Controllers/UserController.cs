using Microsoft.AspNetCore.Mvc;
using SocialSiteWebApplication.Models;

using SocialSiteWebApplication.DataStore;

namespace SocialSiteWebApplication.Controllers;

public class UserController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private DataStore.DataStore _dataStore;
    
    public UserController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _dataStore = DataStore.DataStore.Instance;
    }
    
    public IActionResult Index(string userName)
    {
        var usersFriends = _dataStore.GetUsersFriends(userName);
        return View("Index", new UserViewModel(userName, usersFriends));
    }

}