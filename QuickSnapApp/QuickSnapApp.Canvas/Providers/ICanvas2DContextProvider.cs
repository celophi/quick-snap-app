using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Providers;
public interface ICanvas2DContextProvider
{
    Task BeginBatchAsync();
    Task EndBatchAsync();
    Task BeginPathAsync();
    Task DrawImageAsync(ElementReference elementReference, double dx, double dy);
    Task SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy);
    Task SetFillStyleAsync(object value);
    Task FillRectAsync(double x, double y, double width, double height);
    Task TranslateAsync(double x, double y);
    Task ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null);
    Task FillAsync();
    Task SetFontAsync(string value);
    Task SetTextBaselineAsync(TextBaseline value);
    Task SetTextAlignAsync(TextAlign value);
    Task RotateAsync(float angle);
    Task FillTextAsync(string text, double x, double y, double? maxWidth = null);
    Task LineToAsync(double x, double y);
    Task MoveToAsync(double x, double y);
    Task StrokeAsync();
    Task SetStrokeStyleAsync(string value);
    Task SetLineWidthAsync(float value);
    Task SetLineCapAsync(LineCap value);
}
