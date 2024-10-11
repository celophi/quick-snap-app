using Microsoft.Extensions.DependencyInjection;

namespace QuickSnapApp.Canvas;
public sealed class DependencyProvider
{
    private readonly IServiceCollection _serviceCollection;

    public DependencyProvider(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public void Register()
    {
        //_serviceCollection.AddTransient<ICanvasFactory, CanvasFactory>();

        //_serviceCollection
        //    .AddTransient<IDateTimeProvider, DateTimeProvider>()
        //    .AddTransient<ITaskRunProvider, TaskRunProvider>()
        //    .AddTransient<ITaskDelayProvider, TaskDelayProvider>()
        //    .AddTransient<ICancellationTokenProvider, CancellationTokenProvider>();
    }
}
