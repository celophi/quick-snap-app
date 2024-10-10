
using Blazor.Extensions.Canvas.Canvas2D;

namespace QuickSnappApp.Canvas.Providers;
public sealed class CanvasContextProvider : ICanvasContextProvider
{
    private Canvas2DContext _context;

    public CanvasContextProvider(Canvas2DContext context)
    {
        _context = context;
    }

    public Task SetFillStyleAsync(object value) =>
        _context.SetFillStyleAsync(value);

    public Task SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy) =>
        _context.SetTransformAsync(m11, m12, m21, m22, dx, dy);
}
