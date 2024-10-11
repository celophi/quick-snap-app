using QuickSnapApp.Canvas.Providers;

namespace QuickSnapApp.Canvas;

public sealed class BECanvasFactory : IBECanvasFactory
{
    public IBECanvasProvider Create() => new BECanvasProvider();
}
