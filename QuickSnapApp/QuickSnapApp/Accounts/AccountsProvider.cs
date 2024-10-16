﻿using Microsoft.Extensions.Options;
using QuickSnapApp.Configuration;
using System.Net.Http.Json;

namespace QuickSnapApp.Accounts;
public sealed class AccountsProvider(HttpClient _httpClient, IOptions<ApiOptions> _apiOptions) : IAccountsProvider
{
    public async Task<AccountsRegisterResponseViewModel> RegisterAsync(AccountsRegisterRequestViewModel request)
    {
        try
        {
            var url = _apiOptions.Value.ApiUrl;
            var response = await _httpClient.PostAsJsonAsync($"{_apiOptions.Value.ApiUrl}/api/accounts/register", request);
            return (await response.Content.ReadFromJsonAsync<AccountsRegisterResponseViewModel>())!;
        }
        catch (Exception ex)
        {
            var a = ex;
            throw;
        }
    }
}
