namespace CoolCameraApp.Providers;
public interface ITaskDelayProvider
{
    /// <summary>
    /// Delays for the specified time.
    /// </summary>
    /// <param name="millisecondsDelay"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delay(int millisecondsDelay, CancellationToken cancellationToken = default);
}
