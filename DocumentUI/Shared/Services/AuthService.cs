namespace DocumentUI.Services;
public interface IAuthService
{
    bool IsAuthenticated { get; }
    string UserName { get; }
    string UserId { get; }
    string RoleName { get; }
    void MarkUserAsAuthenticated(string userName, string userId, string roleName);
    void MarkUserAsLoggedOut();
}
public class AuthService : IAuthService
{
    public bool IsAuthenticated => !string.IsNullOrEmpty(UserName);

    public string UserName { get; private set; }

    public string UserId { get; private set; }

    public string RoleName { get; private set; }

    public void MarkUserAsAuthenticated(string userName, string userId, string roleName)
    {
        UserName = userName;
        UserId = userId;
        RoleName = roleName;
    }

    public void MarkUserAsLoggedOut()
    {
        UserName = null;
        UserId = null;
        RoleName = null;
    }
}