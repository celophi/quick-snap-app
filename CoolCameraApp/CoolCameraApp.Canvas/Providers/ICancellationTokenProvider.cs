namespace CoolCameraApp.Canvas.Providers;
internal interface ICancellationTokenProvider
{
    /// <summary>
    /// Returns true if the operation has been requested to cancel
    /// </summary>
    /// <param name="cancellationTokenSource"></param>
    bool IsCancellationRequested(CancellationTokenSource cancellationTokenSource);
}
