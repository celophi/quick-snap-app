namespace QuickSnapApp.Accounts;
public interface IAccountsRepository
{
    Task RegisterAsync(string username, string password);

    Task<bool> HasAccountAsync();

    Task DeleteAsync();
}
