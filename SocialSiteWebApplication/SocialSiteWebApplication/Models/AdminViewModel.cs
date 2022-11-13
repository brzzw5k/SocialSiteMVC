using SocialSiteWebApplication.DataStore;

namespace SocialSiteWebApplication.Models;

public class AdminViewModel
{
    public List<User> Users { get; set; }
    
    public AdminViewModel(List<User> users)
    {
        Users = users;
    }
}