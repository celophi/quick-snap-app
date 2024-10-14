using QuickSnapApp.Canvas.Providers;
using QuickSnapApp.Providers;

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
            .AddTransient<ICancellationTokenSourceProvider, CancellationTokenSourceProvider>();

        _serviceCollection.AddTransient(sp => MediaPicker.Default);
    }
}
