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
            .AddTransient<ICancellationTokenProvider, CancellationTokenProvider>()
            .AddTransient<IPermissionsProvider, PermissionsProvider>()
            .AddTransient<IMathProvider, MathProvider>();

        _serviceCollection.AddTransient(sp => MediaPicker.Default);
    }
}
