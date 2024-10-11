using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Providers;
public interface IBECanvasProvider
{
    Task<ICanvas2DContextProvider> GetCanvas2DAsync();

    ElementReference GetCanvasReference();
}
