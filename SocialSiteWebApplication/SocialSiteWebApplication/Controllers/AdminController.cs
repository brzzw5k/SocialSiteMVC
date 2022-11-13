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
    
    public IActionResult Index()
    {
        var users = _dataStore.GetUsers();
        return View("Index", new AdminViewModel(users));
    }
    
    public IActionResult Initialize()
    {
        _dataStore.Initialize();
        return RedirectToAction("Index");
    }

    public IActionResult RedirectToAddUser(string newUserName)
    {
        return RedirectToAction("AddUser", "User", new {userName = newUserName});
    }
    
    public IActionResult RemoveUser(string userName)
    {
        try
        {
            _dataStore.RemoveUser(userName);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message});
        }
    }
}