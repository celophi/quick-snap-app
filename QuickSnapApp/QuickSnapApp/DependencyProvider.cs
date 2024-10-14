using QuickSnapApp.Canvas.Providers;
using QuickSnapApp.Providers;

namespace QuickSnapApp;
public sealed class DependencyProvider
{
    private readonly IServiceCollection _serviceCollection;

    public DependencyProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

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
