using SocialSiteWebApplication.DataStore;

namespace SocialSiteWebApplication.Models;

public class AdminViewModel
{
    public string NewUserName { get; set; }
    public List<User> Users { get; set; }
    
    public AdminViewModel(List<User> users)
    {
        NewUserName = string.Empty;
        Users = users;
    }
}