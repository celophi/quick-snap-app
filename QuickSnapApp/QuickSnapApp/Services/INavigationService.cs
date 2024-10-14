using QuickSnapApp.Providers;

namespace QuickSnapApp.Services;
public interface INavigationService
{
    /// <summary>
    /// Registers the concrete navigation manager to the provider.
    /// This is necessary in order for XAML views (which do not have access to a BlazorWebView) to know how to navigate.
    /// </summary>
    /// <param name="navigationManagerProvider"></param>
    void RegisterNavigationManagerProvider(INavigationManagerProvider navigationManagerProvider);

    /// <summary>
    /// Navigates from a Blazor page to another Blazor page.
    /// </summary>
    /// <param name="uri"></param>
    void NavigateToBlazor(string uri);

    /// <summary>
    /// Navigates from either a Blazor or XAML page to a XAML page.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    Task NavigateToXamlAsync(string uri);

    /// <summary>
    /// Navigates from a XAML page to a Blazor page.
    /// This method will reset the navigation stack for XAML up to the root view.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    Task NavigateFromXamlToBlazor(string uri);
}
