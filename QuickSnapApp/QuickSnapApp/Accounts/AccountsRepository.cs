using QuickSnapApp.Providers;

namespace QuickSnapApp.Accounts;
public sealed class AccountsRepository(
    IAccountsProvider _accountsProvider,
    ISecureStorageProvider _secureStorageProvider,
    IDeviceInfoProvider _deviceInfoProvider) : IAccountsRepository
{
    private const string AuthTokenKey = "authToken";

    public async Task RegisterAsync(string username, string password)
    {
        var deviceName = _deviceInfoProvider.Name();
        var deviceManufacturer = _deviceInfoProvider.Manufacturer();

        var response = await _accountsProvider.RegisterAsync(new AccountsRegisterRequestViewModel
        {
            DeviceManufacturer = deviceManufacturer,
            DeviceName = deviceName,
            Username = username,
            Password = password,
        });

        await _secureStorageProvider.SetAsync(AuthTokenKey, response.Token);
    }

    public async Task<bool> HasAccountAsync()
    {
        var token = await _secureStorageProvider.GetAsync(AuthTokenKey);
        return token is not null;
    }

    public async Task DeleteAsync()
    {
        _secureStorageProvider.Remove(AuthTokenKey);
        await Task.CompletedTask;
    }
}
