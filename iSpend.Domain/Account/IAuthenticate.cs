namespace iSpend.Domain.Account;

public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);

    Task<bool> Register(string name, string email, string password);

    Task<IEnumerable<string>> GetUserNameAndId(string email);

    Task Logout();
}
