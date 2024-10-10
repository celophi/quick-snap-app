using QuickSnappApp.Canvas.Providers;

namespace QuickSnappApp.Canvas;
internal interface ICanvasFactory
{
    ICanvasProvider Create();
}
