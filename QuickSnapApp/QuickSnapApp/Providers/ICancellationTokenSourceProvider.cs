namespace QuickSnapApp.Providers;
public interface ICancellationTokenSourceProvider
{
    bool IsCancellationRequested();

    CancellationToken Token();

    void Cancel();
}
