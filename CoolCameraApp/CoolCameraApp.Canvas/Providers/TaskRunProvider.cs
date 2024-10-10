namespace CoolCameraApp.Canvas.Providers;
internal sealed class TaskRunProvider : ITaskRunProvider
{
    /// <inheritdoc/>
    public Task Run(Action action, CancellationToken cancellationToken = default) =>
        Task.Run(action, cancellationToken);
}
