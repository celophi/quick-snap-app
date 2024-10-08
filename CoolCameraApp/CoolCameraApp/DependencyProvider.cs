namespace CoolCameraApp;
public sealed class DependencyProvider
{
    private readonly IServiceCollection _serviceCollection;

    public DependencyProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public void Register()
    {
        //_serviceCollection
        //    .AddTransient(sp => MediaPicker.Default)
        //    .AddTransient<IPermissionsProvider, PermissionsProvider>()
        //    .AddTransient<INavigationManagerProvider, NavigationManagerProvider>()
        //    .AddTransient<IShellProvider, ShellProvider>();

        //_serviceCollection.AddTransient<Components.Pages.Camera>();

        //// This is registered as a singleton in order for all XAML pages
        //// to always use the navigation manager that is linked to the top-level BlazorWebView.
        //_serviceCollection.AddSingleton<INavigationService, NavigationService>();
    }
}
