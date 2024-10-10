namespace QuickSnappApp.Providers;
public sealed class TaskDelayProvider : ITaskDelayProvider
{
    /// <inheritdoc/>
    public Task Delay(int millisecondsDelay, CancellationToken cancellationToken) =>
        Task.Delay(millisecondsDelay, cancellationToken);
}
