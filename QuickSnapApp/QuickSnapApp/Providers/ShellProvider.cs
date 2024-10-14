namespace QuickSnapApp.Providers;
public sealed class ShellProvider : IShellProvider
{
    /// <inheritdoc/>
    public async Task GoToAsync(ShellNavigationState state) =>
        await Shell.Current.GoToAsync(state);

    /// <inheritdoc/>
    public async Task PopToRootAsync() =>
        await Shell.Current.Navigation.PopToRootAsync();
}

