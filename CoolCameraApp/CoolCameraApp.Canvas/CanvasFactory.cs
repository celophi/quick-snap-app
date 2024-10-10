using Blazor.Extensions.Canvas;
using QuickSnappApp.Canvas.Providers;

namespace QuickSnappApp.Canvas;
internal class CanvasFactory : ICanvasFactory
{
    public ICanvasProvider Create()
    {
        return new CanvasProvider(new BECanvas());
    }
}
