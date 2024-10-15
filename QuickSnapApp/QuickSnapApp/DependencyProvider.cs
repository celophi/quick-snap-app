using QuickSnapApp.Accounts;
using QuickSnapApp.Canvas.Providers;
using QuickSnapApp.Pictures;
using QuickSnapApp.Providers;
using QuickSnapApp.Services;

namespace QuickSnapApp;
public sealed class DependencyProvider(IServiceCollection _serviceCollection)
{
    public void Register()
    {
        this.RegisterTransients();
        this.RegisterPages();
        this.RegisterSingletons();
    }

    private void RegisterTransients()
    {
        _serviceCollection
            .AddTransient<IDateTimeProvider, DateTimeProvider>()
            .AddTransient<ITaskRunProvider, TaskRunProvider>()
            .AddTransient<ITaskDelayProvider, TaskDelayProvider>()
            .AddTransient<IPermissionsProvider, PermissionsProvider>()
            .AddTransient<IMathProvider, MathProvider>()
            .AddTransient<ICancellationTokenSourceProvider, CancellationTokenSourceProvider>()
            .AddTransient<IShellProvider, ShellProvider>()
            .AddTransient<INavigationManagerProvider, NavigationManagerProvider>()
            .AddTransient<IDeviceInfoProvider, DeviceInfoProvider>()
            .AddTransient<IAccountsProvider, AccountsProvider>()
            .AddTransient<ISecureStorageProvider, SecureStorageProvider>()
            .AddTransient<IAccountsRepository, AccountsRepository>()
            .AddTransient<IPicturesProvider, PicturesProvider>();

        _serviceCollection.AddTransient(sp => MediaPicker.Default);
    }

    private void RegisterSingletons()
    {
        // This is registered as a singleton in order for all XAML pages
        // to always use the navigation manager that is linked to the top-level BlazorWebView.
        _serviceCollection.AddSingleton<INavigationService, NavigationService>();
    }

    private void RegisterPages()
    {
        _serviceCollection.AddTransient<Components.Pages.ToolkitCamera>();
    }
}
