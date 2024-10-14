using Microsoft.AspNetCore.Components;
using QuickSnapApp.Providers;

namespace QuickSnapApp.Components.Pages;
public sealed partial class Clock : ComponentBase, IDisposable
{
    [Inject]
    private ITaskRunProvider _taskRunProvider { get; init; } = default!;

    [Inject]
    private ITaskDelayProvider _taskDelayProvider { get; init; } = default!;

    [Inject]
    private IDateTimeProvider _dateTimeProvider { get; init; } = default!;

    [Inject]
    private ICancellationTokenSourceProvider _cancellationTokenSourceProvider { get; init; } = default!;

    /// <summary>
    /// Digital clock time.
    /// </summary>
    private string timeOfDay = string.Empty;

    /// <summary>
    /// Ongoing task that performs continual rendering.
    /// </summary>
    private Task? _renderTask;

    /// <summary>
    /// Synchronized time to display for both clocks.
    /// </summary>
    private DateTime? renderedTime;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _renderTask = _taskRunProvider.Run(async () =>
            {
                while (!_cancellationTokenSourceProvider.IsCancellationRequested())
                {
                    try
                    {
                        await RenderAsync();
                    }
                    catch (OperationCanceledException)
                    {
                        // The operation was canceled. No further action is required.
                    }
                }
            }, _cancellationTokenSourceProvider.Token());
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Renders both analog and digital clocks.
    /// </summary>
    /// <returns></returns>
    private async Task RenderAsync()
    {
        // Determine the starting time and time to render for both clocks.
        renderedTime = _dateTimeProvider.Now();
        timeOfDay = renderedTime.GetValueOrDefault().ToString("h:mm:ss tt");

        // This is necessary for both clocks to be re-rendered.
        await InvokeAsync(StateHasChanged);

        // Measure the time taken for all rendering and apply a floor of at least 1 second delay.
        // This helps make the clock rendering look consistent.
        const int targetDelayMs = 1000;
        var elapsed = _dateTimeProvider.Now() - renderedTime.GetValueOrDefault();
        var remainingDelay = targetDelayMs - (int)elapsed.TotalMilliseconds;

        if (remainingDelay > 0)
        {
            await _taskDelayProvider.Delay(remainingDelay, _cancellationTokenSourceProvider.Token());
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cancellationTokenSourceProvider.Cancel();
    }
}
