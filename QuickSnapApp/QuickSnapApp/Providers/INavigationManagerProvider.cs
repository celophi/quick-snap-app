namespace QuickSnapApp.Providers;
public interface INavigationManagerProvider
{
    /// <summary>
    /// Navigates to a new page within Blazor.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="forceLoad"></param>
    void NavigateTo(string uri, bool forceLoad = false);
}
