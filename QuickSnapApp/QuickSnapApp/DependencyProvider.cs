using QuickSnapApp.Canvas.Providers;
using QuickSnapApp.Providers;
using QuickSnapApp.Services;

namespace QuickSnapApp;
public sealed class DependencyProvider(IServiceCollection _serviceCollection)
{
    public void Register()
    {
        _serviceCollection
            .AddTransient<IDateTimeProvider, DateTimeProvider>()
            .AddTransient<ITaskRunProvider, TaskRunProvider>()
            .AddTransient<ITaskDelayProvider, TaskDelayProvider>()
            .AddTransient<IPermissionsProvider, PermissionsProvider>()
            .AddTransient<IMathProvider, MathProvider>()
            .AddTransient<ICancellationTokenSourceProvider, CancellationTokenSourceProvider>()
            .AddTransient<IShellProvider, ShellProvider>()
            .AddTransient<INavigationManagerProvider, NavigationManagerProvider>();

        _serviceCollection.AddTransient(sp => MediaPicker.Default);

        _serviceCollection.AddTransient<Components.Pages.AdvancedCamera>();

        // This is registered as a singleton in order for all XAML pages
        // to always use the navigation manager that is linked to the top-level BlazorWebView.
        _serviceCollection.AddSingleton<INavigationService, NavigationService>();
    }
}
