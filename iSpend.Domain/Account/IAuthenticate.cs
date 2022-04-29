namespace iSpend.Domain.Account;

public interface IAuthenticate
{
    Task<bool> AuthenticateAsync(string email, string password);

    Task<bool> RegisterAsync(string email, string password);

    Task Logout();
}
