using SocialSiteWebApplication.DataStore;

namespace SocialSiteWebApplication.Models;

public class UserViewModel
{
    public string UserName { get; set; }
    public string NewFriendUserName { get; set; }
    public List<User> Friends { get; set; }
    
    public UserViewModel(string userName, List<User> friends)
    {
        UserName = userName;
        NewFriendUserName = string.Empty;
        Friends = friends;
    }
}