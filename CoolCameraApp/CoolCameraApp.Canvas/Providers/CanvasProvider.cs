using Blazor.Extensions.Canvas;

namespace QuickSnappApp.Canvas.Providers;
internal class CanvasProvider : ICanvasProvider
{
    private readonly BECanvas _canvas;

    public CanvasProvider(BECanvas canvas)
    {
        _canvas = canvas;
    }
}
