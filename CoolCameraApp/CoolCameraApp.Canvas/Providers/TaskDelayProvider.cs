namespace CoolCameraApp.Canvas.Providers;
internal sealed class TaskDelayProvider : ITaskDelayProvider
{
    /// <inheritdoc/>
    public Task Delay(int millisecondsDelay, CancellationToken cancellationToken) =>
        Task.Delay(millisecondsDelay, cancellationToken);
}
