namespace QuickSnappApp.Canvas.Providers;
public interface ICanvasContextProvider
{
    Task SetTransformAsync(double m11, double m12, double m21, double m22, double dx, double dy);
    Task SetFillStyleAsync(object value);
}
