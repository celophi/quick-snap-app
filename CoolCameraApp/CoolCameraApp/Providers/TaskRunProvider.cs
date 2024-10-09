namespace CoolCameraApp.Providers;
public sealed class TaskRunProvider : ITaskRunProvider
{
    /// <inheritdoc/>
    public Task Run(Action action, CancellationToken cancellationToken = default) =>
        Task.Run(action, cancellationToken);
}
