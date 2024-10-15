using Microsoft.AspNetCore.Components;
using QuickSnapApp.Accounts;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Pages;
public partial class Home
{
    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    [Inject]
    private IAccountsRepository _accountsRepository { get; init; } = default!;

    public async Task OnNavigateClock()
    {
        _navigationService.NavigateToBlazor("/clock");
        await Task.CompletedTask;
    }

    public async Task OnNavigateNativeCamera()
    {
        _navigationService.NavigateToBlazor("/native-camera");
        await Task.CompletedTask;
    }

    public async Task OnNavigateToolkitCamera()
    {
        await _navigationService.NavigateToXamlAsync("/toolkit-camera");
    }

    public async Task OnDeleteAccount()
    {
        await _accountsRepository.DeleteAsync();
        _navigationService.NavigateToBlazor("/register");
    }
}
