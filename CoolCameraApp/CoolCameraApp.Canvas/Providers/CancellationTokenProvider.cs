namespace CoolCameraApp.Canvas.Providers;
internal sealed class CancellationTokenProvider : ICancellationTokenProvider
{
    public bool IsCancellationRequested(CancellationTokenSource cancellationTokenSource) =>
        cancellationTokenSource.IsCancellationRequested;
}
