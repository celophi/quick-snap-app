namespace QuickSnapApp.Providers;
public sealed class SecureStorageProvider : ISecureStorageProvider
{
    public async Task SetAsync(string key, string value) =>
        await SecureStorage.SetAsync(key, value);

    public async Task<string?> GetAsync(string key) =>
        await SecureStorage.GetAsync(key);

    public void Remove(string key) =>
        SecureStorage.Remove(key);
}
