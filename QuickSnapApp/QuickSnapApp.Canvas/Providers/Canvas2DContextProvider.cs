using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Providers;
public sealed class Canvas2DContextProvider(Canvas2DContext canvas2DContext) : ICanvas2DContextProvider
{
    public Task ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null)
    {
        throw new NotImplementedException();
    }

    public Task BeginBatchAsync()
    {
        throw new NotImplementedException();
    }

    public Task BeginPathAsync()
    {
        throw new NotImplementedException();
    }

    public Task DrawImageAsync(ElementReference elementReference, double dx, double dy)
    {
        throw new NotImplementedException();
    }

    public Task EndBatchAsync()
    {
        throw new NotImplementedException();
    }

    public Task FillAsync()
    {
        throw new NotImplementedException();
    }

    public Task FillRectAsync(double x, double y, double width, double height)
    {
        throw new NotImplementedException();
    }

    public Task FillTextAsync(string text, double x, double y, double? maxWidth = null)
    {
        throw new NotImplementedException();
    }

    public Task LineToAsync(double x, double y)
    {
        throw new NotImplementedException();
    }

    public Task MoveToAsync(double x, double y)
    {
        throw new NotImplementedException();
    }

    public Task RotateAsync(float angle)
    {
        throw new NotImplementedException();
    }

    public Task SetFillStyleAsync(object value)
    {
        throw new NotImplementedException();
    }

    public Task SetFontAsync(string value)
    {
        throw new NotImplementedException();
    }

    public Task SetLineCapAsync(LineCap value)
    {
        throw new NotImplementedException();
    }

    public Task SetLineWidthAsync(float value)
    {
        throw new NotImplementedException();
    }

    public Task SetStrokeStyleAsync(string value)
    {
        throw new NotImplementedException();
    }

    public Task SetTextAlignAsync(TextAlign value)
    {
        throw new NotImplementedException();
    }

    public Task SetTextBaselineAsync(TextBaseline value)
    {
        throw new NotImplementedException();
    }

    public Task SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy)
    {
        throw new NotImplementedException();
    }

    public Task StrokeAsync()
    {
        throw new NotImplementedException();
    }

    public Task TranslateAsync(double x, double y)
    {
        throw new NotImplementedException();
    }
}
