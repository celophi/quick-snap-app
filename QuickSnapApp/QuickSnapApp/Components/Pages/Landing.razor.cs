using Microsoft.AspNetCore.Components;
using QuickSnapApp.Accounts;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Pages;
public partial class Landing : ComponentBase
{
    [Inject]
    private IAccountsRepository _accountsRepository { get; init; } = default!;

    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var hasAccount = await _accountsRepository.HasAccountAsync();

        if (hasAccount)
        {
            _navigationService.NavigateToBlazor("/home");
        }
        else
        {
            _navigationService.NavigateToBlazor("/register");
        }
    }
}
