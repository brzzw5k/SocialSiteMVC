using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using SocialSiteWebApplication.DataStore;
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
    
    [Route("User/List")]
    public IActionResult Index()
    {
        var users = _dataStore.GetUsers();
        return View("Index", new AdminViewModel(users));
    }
    
    [Route("Init")]
    public IActionResult Initialize()
    {
        _dataStore.Initialize();
        return RedirectToAction("Index");
    }
    
    [Route("User/Add/{newUserName}")]
    public IActionResult RedirectToAddUser(string newUserName)
    {
        return RedirectToAction("AddUser", "User", new {userName = newUserName});
    }
    
    [Route("User/Del/{userName}")]
    public IActionResult RedirectToRemoveUser(string userName)
    {
        return RedirectToAction("RemoveUser", "User", new {userName = userName});
    }
}