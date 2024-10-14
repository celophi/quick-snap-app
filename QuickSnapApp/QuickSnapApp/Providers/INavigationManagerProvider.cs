namespace QuickSnapApp.Providers;
public interface INavigationManagerProvider
{
    /// <summary>
    /// Navigates to a new page within Blazor.
    /// </summary>
    /// <param name="uri"></param>
    void NavigateTo(string uri);
}
