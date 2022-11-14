using System.Diagnostics;
using System.Text;
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
    
    [Route("Friends")]
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
    
    public IActionResult RemoveUser(string userName)
    {
        try
        {
            _dataStore.RemoveUser(userName);
            return RedirectToAction("Index", "Admin");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message});
        }
    }
    
    [Route("Friends/Add/{friendName}")]
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
    
    [Route("Friends/Del/{friendName}")]
    public IActionResult RemoveFriend(string userName, string friendName)
    {
        try
        {
            _dataStore.RemoveUserFriend(userName, friendName);
            return RedirectToAction("Index", new { userName = userName });
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message });
        }
    }
    
    [Route("Friends/Export")]
    public IActionResult ExportFriends(string userName)
    {
        try
        {
            var usersFriends = _dataStore.GetUsersFriends(userName);
            var csv = new StringBuilder();
            csv.AppendLine("Name");
            foreach (var friend in usersFriends)
            {
                csv.AppendLine(friend.ToString());
            }
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "Friends.csv");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message });
        }
    }
    
    [Route("Friends/Import")]
    public IActionResult ImportFriends(string userName)
    {
        try
        {
            return View("Index", new UserViewModel(userName, new List<User>()));
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message });
        }
    }
    
}