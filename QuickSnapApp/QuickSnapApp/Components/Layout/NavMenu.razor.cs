using Microsoft.AspNetCore.Components;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Layout;
public partial class NavMenu
{
    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    private async Task OnNavigateAdvCamera()
    {
        await _navigationService.NavigateToXamlAsync("advanced-camera");
    }
}
