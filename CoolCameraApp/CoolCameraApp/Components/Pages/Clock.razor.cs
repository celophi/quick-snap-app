using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using CoolCameraApp.Providers;
using Microsoft.AspNetCore.Components;
using LineCap = Blazor.Extensions.Canvas.Canvas2D.LineCap;

namespace CoolCameraApp.Components.Pages;
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

    private const string grayColor = "#f8f9fa";
    private const string blueColor = "#007bff";

    /// <summary>
    /// 2D context for staging.
    /// </summary>
    private Canvas2DContext? _stagingContext;

    /// <summary>
    /// 2D context for displaying
    /// </summary>
    private Canvas2DContext? _targetContext;

    /// <summary>
    /// Invisible canvas that is used to stage all drawing operations.
    /// </summary>
    private BECanvas? stagingCanvas;

    /// <summary>
    /// Visible canvas that receives all drawings that are staged.
    /// </summary>
    private BECanvas? targetCanvas;

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

    /// <summary>
    /// Maximum width of the clock.
    /// </summary>
    private const int clockWidth = 400;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _renderTask = _taskRunProvider.Run(async () =>
            {
                _stagingContext = await stagingCanvas.CreateCanvas2DAsync();
                _targetContext = await targetCanvas.CreateCanvas2DAsync();

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
        await DrawClock();

        // Copy the drawing from the invisible canvas to the visible one to prevent the user from seeing artificats during the drawing process.
        await _targetContext!.DrawImageAsync(stagingCanvas!.CanvasReference, 0, 0);

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

    /// <summary>
    /// Draws the entire clock.
    /// </summary>
    /// <returns></returns>
    private async Task DrawClock()
    {
        await _stagingContext!.SetTransformAsync(1, 0, 0, 1, 0, 0);

        await _stagingContext.SetFillStyleAsync("white");
        await _stagingContext.FillRectAsync(0, 0, clockWidth, clockWidth);

        float radius = clockWidth / 2;
        await _stagingContext.TranslateAsync(radius, radius);
        radius = radius * 0.9f;

        await _stagingContext.ArcAsync(0, 0, radius, 0, 2 * Math.PI);
        await _stagingContext.SetFillStyleAsync(blueColor);
        await _stagingContext.FillAsync();

        await DrawFaceAsync(radius);
        await DrawNumbers(radius);
        await DrawTime(radius);
    }

    /// <summary>
    /// Draws the clock face.
    /// </summary>
    /// <param name="radius">Radius of the clock face</param>
    /// <returns></returns>
    private async Task DrawFaceAsync(float radius)
    {
        // Draw the big blue circle.
        await _stagingContext!.BeginPathAsync();
        await _stagingContext.ArcAsync(0, 0, radius, 0, 2 * Math.PI);
        await _stagingContext.SetFillStyleAsync(blueColor);
        await _stagingContext.FillAsync();

        // Draw a smaller gray circle for the dials
        await _stagingContext.BeginPathAsync();
        await _stagingContext.ArcAsync(0, 0, radius * 0.1, 0, 2 * Math.PI);
        await _stagingContext.SetFillStyleAsync(grayColor);
        await _stagingContext.FillAsync();
    }

    /// <summary>
    /// Draws the numbers 1 - 12 on the clock face
    /// </summary>
    /// <param name="radius">Radius of the clock face</param>
    /// <returns></returns>
    private async Task DrawNumbers(float radius)
    {
        await _stagingContext!.SetFillStyleAsync("white");
        await _stagingContext.SetFontAsync($"{radius * 0.15}px arial");
        await _stagingContext.SetTextBaselineAsync(TextBaseline.Middle);
        await _stagingContext.SetTextAlignAsync(TextAlign.Center);

        for (var num = 1; num <= 12; num++)
        {
            float angle = Convert.ToSingle(num * Math.PI / 6);
            await _stagingContext.RotateAsync(angle);
            await _stagingContext.TranslateAsync(0, -radius * 0.85);
            await _stagingContext.RotateAsync(-angle);
            await _stagingContext.FillTextAsync(num.ToString(), 0, 0);
            await _stagingContext.RotateAsync(angle);
            await _stagingContext.TranslateAsync(0, radius * 0.85);
            await _stagingContext.RotateAsync(-angle);
        }
    }

    /// <summary>
    /// Draws the current time on the clock
    /// </summary>
    /// <param name="radius">Radius of the clock face</param>
    /// <returns></returns>
    private async Task DrawTime(float radius)
    {
        // Calculate hour angle and convert 24-hour format to 12-hour
        var hour = renderedTime.Hour % 12;
        var minute = renderedTime.Minute;
        var second = renderedTime.Second;

        var hourAngle = (hour * Math.PI / 6) + (minute * Math.PI / (6 * 60)) + (second * Math.PI / (360 * 60));
        await DrawHand(Convert.ToSingle(hourAngle), radius * 0.5f, radius * 0.07f);

        // Calculate minute angle
        var minuteAngle = (minute * Math.PI / 30) + (second * Math.PI / (30 * 60));
        await DrawHand(Convert.ToSingle(minuteAngle), radius * 0.8f, radius * 0.07f);

        // Calculate second angle
        var secondAngle = second * Math.PI / 30;
        await DrawHand(Convert.ToSingle(secondAngle), radius * 0.9f, radius * 0.02f);
    }

    /// <summary>
    /// Draws the clock hands.
    /// </summary>
    /// <param name="angle">Angle to draw the line</param>
    /// <param name="length">Length of the line</param>
    /// <param name="width">Width of the line</param>
    /// <returns></returns>
    private async Task DrawHand(float angle, float length, float width)
    {
        await _stagingContext!.SetStrokeStyleAsync(grayColor);
        await _stagingContext.BeginPathAsync();
        await _stagingContext.SetLineWidthAsync(width);
        await _stagingContext.SetLineCapAsync(LineCap.Round);
        await _stagingContext.MoveToAsync(0, 0);
        await _stagingContext.RotateAsync(angle);
        await _stagingContext.LineToAsync(0, -length);
        await _stagingContext.StrokeAsync();
        await _stagingContext.RotateAsync(-angle);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
    }
}
