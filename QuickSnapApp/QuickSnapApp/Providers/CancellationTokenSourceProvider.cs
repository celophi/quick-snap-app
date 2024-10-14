namespace QuickSnapApp.Providers;
public sealed class CancellationTokenSourceProvider() : ICancellationTokenSourceProvider
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public bool IsCancellationRequested() =>
        _cancellationTokenSource.IsCancellationRequested;

    public CancellationToken Token() => _cancellationTokenSource.Token;

    public void Cancel() => _cancellationTokenSource.Cancel();
}
