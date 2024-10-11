namespace QuickSnapApp.Providers;
public sealed class CancellationTokenProvider : ICancellationTokenProvider
{
    public bool IsCancellationRequested(CancellationTokenSource cancellationTokenSource) =>
        cancellationTokenSource.IsCancellationRequested;
}
