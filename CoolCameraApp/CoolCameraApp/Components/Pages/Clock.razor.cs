using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace CoolCameraApp.Components.Pages;
public partial class Clock : ComponentBase
{
    private BECanvas canvasReferenceA;
    private BECanvas canvasReferenceB;

    private bool Visible = false;


    Canvas2DContext _context2D;

    private Task _renderTask;



    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _renderTask = Task.Run(async () =>
            {
                _context2D = await canvasReferenceA.CreateCanvas2DAsync();
                var visibleCanvas = await canvasReferenceB.CreateCanvas2DAsync();

                while (true)
                {
                    try
                    {
                        await DrawAll(true);



                        await visibleCanvas.DrawImageAsync(canvasReferenceA.CanvasReference, 0, 0);
                        await Task.Delay(200);
                        //await InvokeAsync(StateHasChanged);
                    }
                    catch (Exception ex)
                    {
                        var a = ex;
                    }
                }
            });

        }
    }

    private async Task DrawAll(bool flip)
    {
        await _context2D.SetTransformAsync(1, 0, 0, 1, 0, 0);

        await _context2D.SetFillStyleAsync("black");
        await _context2D.FillRectAsync(0, 0, 400, 400);

        double radius = 400 / 2;
        await _context2D.TranslateAsync(radius, radius);
        radius = radius * 0.9;

        await DrawClock(radius);
    }

    private async Task DrawClock(double radius)
    {
        await _context2D.ArcAsync(0, 0, radius, 0, 2 * Math.PI);
        await _context2D.SetFillStyleAsync("white");
        await _context2D.FillAsync();

        await DrawFaceAsync(radius);
        await DrawNumbers(radius);
        await DrawTime(radius);
    }

    private async Task DrawFaceAsync(double radius)
    {
        // no gradient
        await _context2D.BeginPathAsync();
        await _context2D.ArcAsync(0, 0, radius, 0, 2 * Math.PI);
        await _context2D.SetFillStyleAsync("white");
        await _context2D.FillAsync();

        // no stroke
        await _context2D.BeginPathAsync();
        await _context2D.ArcAsync(0, 0, radius * 0.1, 0, 2 * Math.PI);
        await _context2D.SetFillStyleAsync("black");
        await _context2D.FillAsync();
    }

    private async Task DrawNumbers(double radius)
    {
        await _context2D.SetFontAsync($"{radius * 0.15}px arial");
        await _context2D.SetTextBaselineAsync(TextBaseline.Middle);
        await _context2D.SetTextAlignAsync(TextAlign.Center);

        for (var num = 1; num < 13; num++)
        {
            float angle = Convert.ToSingle(num * Math.PI / 6);
            await _context2D.RotateAsync(angle);
            await _context2D.TranslateAsync(0, -radius * 0.85);
            await _context2D.RotateAsync(-angle);
            await _context2D.FillTextAsync(num.ToString(), 0, 0);
            await _context2D.RotateAsync(angle);
            await _context2D.TranslateAsync(0, radius * 0.85);
            await _context2D.RotateAsync(-angle);
        }
    }

    private async Task DrawTime(double radius)
    {
        var now = DateTime.Now;
        var hour = now.Hour;
        var minute = now.Minute;
        var second = now.Second;

        hour = hour % 12;
        var doubleHour = (hour * Math.PI / 6) + (minute * Math.PI / (6 * 60)) + (second * Math.PI / (360 * 60));

        var length = Convert.ToSingle(radius * 0.5);
        var width = Convert.ToSingle(radius * 0.07);
        var floatHour = Convert.ToSingle(doubleHour);

        await DrawHand(floatHour, length, width);

        // minute
        var doubleMinute = (minute * Math.PI / 30) + (second * Math.PI / (30 * 60));
        var floatMinute = Convert.ToSingle(doubleMinute);

        length = Convert.ToSingle(radius * 0.8);
        width = Convert.ToSingle(radius * 0.07);

        await DrawHand(floatMinute, length, width);

        // second

        var doubleSecond = (second * Math.PI / 30);
        var floatSecond = Convert.ToSingle(doubleSecond);

        length = Convert.ToSingle(radius * 0.9);
        width = Convert.ToSingle(radius * 0.02);

        await DrawHand(floatSecond, length, width);
    }

    private async Task DrawHand(float pos, float length, float width)
    {
        await _context2D.BeginPathAsync();
        await _context2D.SetLineWidthAsync(width);
        await _context2D.SetLineCapAsync(Blazor.Extensions.Canvas.Canvas2D.LineCap.Round);
        await _context2D.MoveToAsync(0, 0);
        await _context2D.RotateAsync(pos);
        await _context2D.LineToAsync(0, -length);
        await _context2D.StrokeAsync();
        await _context2D.RotateAsync(-pos);
    }
}
