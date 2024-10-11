using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Providers;
public sealed class Canvas2DContextProvider(Canvas2DContext _canvas2DContext) : ICanvas2DContextProvider
{
    public async Task ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null) =>
        await _canvas2DContext.ArcAsync(x, y, radius, startAngle, endAngle, anticlockwise);

    public async Task BeginBatchAsync() =>
        await _canvas2DContext.BeginBatchAsync();

    public async Task BeginPathAsync() =>
        await _canvas2DContext.BeginPathAsync();

    public async Task DrawImageAsync(ElementReference elementReference, double dx, double dy) =>
        await _canvas2DContext.DrawImageAsync(elementReference, dx, dy);

    public async Task EndBatchAsync() =>
        await _canvas2DContext.EndBatchAsync();

    public async Task FillAsync() =>
        await _canvas2DContext.FillAsync();

    public async Task FillRectAsync(double x, double y, double width, double height) =>
        await _canvas2DContext.FillRectAsync(x, y, width, height);

    public async Task FillTextAsync(string text, double x, double y, double? maxWidth = null) =>
        await _canvas2DContext.FillTextAsync(text, x, y, maxWidth);

    public Task LineToAsync(double x, double y) =>
        _canvas2DContext.LineToAsync(x, y);

    public async Task MoveToAsync(double x, double y) =>
        await _canvas2DContext.MoveToAsync(x, y);

    public async Task RotateAsync(float angle) =>
        await _canvas2DContext.RotateAsync(angle);

    public async Task SetFillStyleAsync(object value) =>
        await _canvas2DContext.SetFillStyleAsync(value);

    public async Task SetFontAsync(string value) =>
        await _canvas2DContext.SetFontAsync(value);

    public async Task SetLineCapAsync(LineCap value) =>
        await _canvas2DContext.SetLineCapAsync(value);

    public async Task SetLineWidthAsync(float value) =>
        await _canvas2DContext.SetLineWidthAsync(value);

    public async Task SetStrokeStyleAsync(string value) =>
        await _canvas2DContext.SetStrokeStyleAsync(value);

    public async Task SetTextAlignAsync(TextAlign value) =>
        await _canvas2DContext.SetTextAlignAsync(value);

    public async Task SetTextBaselineAsync(TextBaseline value) =>
        await _canvas2DContext.SetTextBaselineAsync(value);

    public async Task SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) =>
        await _canvas2DContext.SetTransformAsync(m11, m12, m21, m22, dx, dy);

    public async Task StrokeAsync() =>
        await _canvas2DContext.StrokeAsync();

    public async Task TranslateAsync(double x, double y) =>
        await _canvas2DContext.TranslateAsync(x, y);
}
