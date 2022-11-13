using System.Diagnostics;
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
        try
        {
            var usersFriends = _dataStore.GetUsersFriends(userName);
            return View("Index", new UserViewModel(userName, usersFriends));
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message});
        }
    }
    
    public IActionResult AddUser(string userName)
    {
        try
        {
            _dataStore.AddUser(userName);
            return RedirectToAction("Index", "Admin");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message});
        }
    }
    
    public IActionResult AddFriend(string userName, string friendName)
    {
        try
        {
            _dataStore.AddUserFriend(userName, friendName);
            return RedirectToAction("Index", new { userName = userName });
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message });
        }
    }

}