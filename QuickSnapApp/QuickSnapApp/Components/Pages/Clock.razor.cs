using Microsoft.AspNetCore.Components;
using QuickSnapApp.Canvas;
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
    private ICancellationTokenProvider _cancellationTokenProvider { get; init; } = default!;

    private AnalogClock _analogClock = new();

    private const string grayColor = "#f8f9fa";
    private const string blueColor = "#007bff";

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
    private DateTime renderedTime;

    /// <summary>
    /// Used to cancel the rendering task.
    /// </summary>
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _renderTask = _taskRunProvider.Run(async () =>
            {
                while (!_cancellationTokenProvider.IsCancellationRequested(_cancellationTokenSource))
                {
                    await RenderAsync();
                }
            });
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
        timeOfDay = renderedTime.ToString("h:mm:ss tt");

        // Draw the analog clock
        await _analogClock.DrawAsync(renderedTime);

        // Copy the drawing from the invisible canvas to the visible one to prevent the user from seeing artificats during the drawing process.
        await _analogClock.RenderAsync();

        // This is necessary for the digital clock to be re-rendered.
        await InvokeAsync(StateHasChanged);

        // Measure the time taken for all rendering and apply a floor of at least 1 second delay.
        // This helps make the clock rendering look consistent.
        const int targetDelayMs = 1000;
        var elapsed = _dateTimeProvider.Now() - renderedTime;
        var remainingDelay = targetDelayMs - (int)elapsed.TotalMilliseconds;

        if (remainingDelay > 0)
        {
            await _taskDelayProvider.Delay(remainingDelay, _cancellationTokenSource.Token);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
    }
}
