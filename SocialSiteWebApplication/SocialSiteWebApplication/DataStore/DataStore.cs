using System.Collections.Concurrent;
using System.ComponentModel;

namespace SocialSiteWebApplication.DataStore;

public class DataStore
{
    private static DataStore? _instance;
    private static readonly object Lock = new object();
    private List<User> _users = new List<User>();

    public void Initialize()
    {
        AddUser("User1");
        AddUser("User2");
        AddUser("User3");
        AddUser("User4");
        AddUser("User5");
        
        AddUserFriend("User1", "User2");
        AddUserFriend("User1", "User3");
        AddUserFriend("User1", "User4");
        AddUserFriend("User2", "User3");
        AddUserFriend("User2", "User4");
        AddUserFriend("User3", "User4");
        AddUserFriend("User3", "User5");
        AddUserFriend("User4", "User5");
    }

    public static DataStore Instance
    {
        get
        {
            lock(Lock)
            {
                return _instance ??= new DataStore();
            }
        }
    }
    
    private User? GetUser(string userName)
    {
        return _users.FirstOrDefault(u => u.UserName == userName);
    }
    
    public List<User> GetUsers()
    {
        return _users;
    }
    
    public List<User> GetUsersFriends(string userName)
    {
        var user = GetUser(userName);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        return user.Friends;
    }

    public void AddUser(string userName)
    {
        lock (Lock)
        {
            if (GetUser(userName) != null)
            {
                throw new Exception("User already exists");
            }
        
            _users.Add(new User(userName));
        }
    }
    
    public void AddUserFriend(string userName, string friendUserName)
    {
        lock (Lock)
        {
            var user = GetUser(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
        
            var friend = GetUser(friendUserName);
            if (friend == null)
            {
                throw new Exception("Friend not found");
            }
            
            if (user.Friends.Contains(friend))
            {
                throw new Exception($"{friendUserName} is already a friend of {userName}");
            }
        
            user.Friends.Add(friend);
            friend.Friends.Add(user);
        }
    }
    
    public void RemoveUser(string userName)
    {
        lock (Lock)
        {
            var user = GetUser(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            user.Friends.ForEach(f => f.Friends.Remove(user));
            _users.Remove(user);
        }
    }
    
    public void RemoveUserFriend(string userName, string friendUserName)
    {
        lock (Lock)
        {
            var user = GetUser(userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
        
            var friend = GetUser(friendUserName);
            if (friend == null)
            {
                throw new Exception("Friend not found");
            }

            if (!user.Friends.Contains(friend))
            {
                throw new Exception($"{friendUserName} is not a friend of {userName}");
            }
            
            user.Friends.Remove(friend);
            friend.Friends.Remove(user);
        }
    }
}