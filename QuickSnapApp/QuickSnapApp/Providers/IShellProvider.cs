namespace QuickSnapApp.Providers;
public interface IShellProvider
{
    /// <summary>
    /// Navigates to a XAML page described by the state parameter.
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    Task GoToAsync(ShellNavigationState state);

    /// <summary>
    /// Navigates all the way back to the root XAML view.
    /// </summary>
    /// <returns></returns>
    Task PopToRootAsync();
}
