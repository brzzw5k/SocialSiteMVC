namespace SocialSiteWebApplication.DataStore;

[Serializable]
public class User
{
    public string UserName { get; set; }
    public List<User> Friends { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public User(string userName)
    {
        UserName = userName;
        Friends = new List<User>();
        CreatedDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{UserName}";
    }
}