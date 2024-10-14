using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Providers;
public sealed class NavigationManagerProvider : INavigationManagerProvider
{
    private readonly NavigationManager _navigationManager;

    public NavigationManagerProvider(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    /// <inheritdoc/>
    public void NavigateTo(string uri) =>
        _navigationManager.NavigateTo(uri);
}
