using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using QuickSnapApp.Canvas.Providers;

namespace QuickSnapApp.Canvas;
public sealed partial class AnalogClock : ComponentBase
{
    [Inject]
    private IMathProvider _mathProvider { get; init; } = default!;

    /// <summary>
    /// Width of the clock in pixels.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public int Width { get; set; }

    /// <summary>
    /// Color to apply to the clock background.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string BackgroundColor { get; set; } = default!;

    /// <summary>
    /// Color to apply to clock dials and numbers
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string ForegroundColor { get; set; } = default!;

    /// <summary>
    /// 2D context for staging.
    /// </summary>
    private ICanvas2DContextProvider? _stagingContext;

    /// <summary>
    /// 2D context for displaying
    /// </summary>
    private ICanvas2DContextProvider? _targetContext;

    /// <summary>
    /// Invisible canvas that is used to stage all drawing operations.
    /// </summary>

    public IBECanvasProvider? _stagingCanvas { get; set; }

    /// <summary>
    /// Visible canvas that receives all drawings that are staged.
    /// </summary>

    public IBECanvasProvider? _targetCanvas { get; set; }

    private bool isInitialized = false;

    private double PI => _mathProvider.PI;

    /// <summary>
    /// Creates the contexts for drawing.
    /// </summary>
    /// <returns></returns>
    private async Task SetupAsync()
    {
        _stagingContext = await _stagingCanvas!.GetCanvas2DAsync();
        _targetContext = await _targetCanvas!.GetCanvas2DAsync();
        isInitialized = true;
    }

    /// <summary>
    /// Draws the clock to a buffer.
    /// </summary>
    /// <returns></returns>
    public async Task DrawAsync(DateTime time)
    {
        if (!isInitialized)
        {
            await SetupAsync();
        }

        await _stagingContext!.BeginBatchAsync();
        await _stagingContext.SetTransformAsync(1, 0, 0, 1, 0, 0);

        await _stagingContext.SetFillStyleAsync("white");
        await _stagingContext.FillRectAsync(0, 0, Width, Width);

        float radius = Width / 2;
        await _stagingContext.TranslateAsync(radius, radius);
        radius = radius * 0.9f;

        await _stagingContext.ArcAsync(0, 0, radius, 0, 2 * PI);
        await _stagingContext.SetFillStyleAsync(BackgroundColor);
        await _stagingContext.FillAsync();

        await DrawFaceAsync(radius);
        await DrawNumbers(radius);
        await DrawTime(radius, time);

        await _stagingContext.EndBatchAsync();
    }

    /// <summary>
    /// Copy the drawing from the invisible canvas to the visible one to prevent the user from seeing artificats during the drawing process.
    /// </summary>
    /// <returns></returns>
    public async Task RenderAsync()
    {
        await _targetContext!.DrawImageAsync(_stagingCanvas!.GetCanvasReference(), 0, 0);
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
        await _stagingContext.ArcAsync(0, 0, radius, 0, 2 * PI);
        await _stagingContext.SetFillStyleAsync(BackgroundColor);
        await _stagingContext.FillAsync();

        // Draw a smaller gray circle for the dials
        await _stagingContext.BeginPathAsync();
        await _stagingContext.ArcAsync(0, 0, radius * 0.1, 0, 2 * PI);
        await _stagingContext.SetFillStyleAsync(ForegroundColor);
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
            float angle = Convert.ToSingle(num * PI / 6);

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
    /// <param name="time">Time to display</param>
    /// <returns></returns>
    private async Task DrawTime(float radius, DateTime time)
    {
        // Calculate hour angle and convert 24-hour format to 12-hour
        var hour = time.Hour % 12;
        var minute = time.Minute;
        var second = time.Second;

        var hourAngle = (hour * PI / 6) + (minute * PI / (6 * 60)) + (second * PI / (360 * 60));
        await DrawHand(Convert.ToSingle(hourAngle), radius * 0.5f, radius * 0.07f);

        // Calculate minute angle
        var minuteAngle = (minute * PI / 30) + (second * PI / (30 * 60));
        await DrawHand(Convert.ToSingle(minuteAngle), radius * 0.8f, radius * 0.07f);

        // Calculate second angle
        var secondAngle = second * PI / 30;
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
        await _stagingContext!.SetStrokeStyleAsync(ForegroundColor);
        await _stagingContext.BeginPathAsync();
        await _stagingContext.SetLineWidthAsync(width);
        await _stagingContext.SetLineCapAsync(LineCap.Round);
        await _stagingContext.MoveToAsync(0, 0);
        await _stagingContext.RotateAsync(angle);
        await _stagingContext.LineToAsync(0, -length);
        await _stagingContext.StrokeAsync();
        await _stagingContext.RotateAsync(-angle);
    }
}
