using Microsoft.AspNetCore.Components;
using QuickSnapApp.Providers;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components;
public partial class Routes
{
    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    [Inject]
    private INavigationManagerProvider _navigationManagerProvider { get; init; } = default!;

    protected override void OnInitialized()
    {
        // Registers the Blazor navigation provider so that it can be used from XAML.
        _navigationService.RegisterNavigationManagerProvider(_navigationManagerProvider);
    }
}
