using Microsoft.Extensions.Options;
using QuickSnapApp.API;
using QuickSnapApp.Configuration;
using System.Net.Http.Json;

namespace QuickSnapApp.Accounts;
public sealed class AccountsProvider(HttpClient _httpClient, IOptions<ApiOptions> _apiOptions) : IAccountsProvider
{
    public async Task<AccountsRegisterResponseViewModel> RegisterAsync(AccountsRegisterRequestViewModel request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiOptions.Value.ApiUrl}/api/accounts", request);
        return (await response.Content.ReadFromJsonAsync<AccountsRegisterResponseViewModel>())!;
    }
}
