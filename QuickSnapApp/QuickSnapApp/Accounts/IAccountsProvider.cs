namespace QuickSnapApp.Accounts;
public interface IAccountsProvider
{
    /// <summary>
    /// Registers a new accounts.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AccountsRegisterResponseViewModel> RegisterAsync(AccountsRegisterRequestViewModel request);
}
