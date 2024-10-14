using QuickSnapApp.Providers;

namespace QuickSnapApp.Services;
public sealed class NavigationService(IShellProvider shellProvider) : INavigationService
{
    private INavigationManagerProvider? _navigationManagerProvider;

    /// <inheritdoc/>
    public void RegisterNavigationManagerProvider(INavigationManagerProvider navigationManagerProvider)
    {
        _navigationManagerProvider = navigationManagerProvider;
    }

    /// <inheritdoc/>
    public void NavigateToBlazor(string uri)
    {
        _navigationManagerProvider!.NavigateTo(uri);
    }

    /// <inheritdoc/>
    public async Task NavigateToXamlAsync(string uri)
    {
        await shellProvider.GoToAsync(uri);
    }

    /// <inheritdoc/>
    public async Task NavigateFromXamlToBlazor(string uri)
    {
        _navigationManagerProvider!.NavigateTo(uri);
        await shellProvider.PopToRootAsync();
    }
}
