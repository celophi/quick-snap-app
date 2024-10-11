﻿using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Microsoft.AspNetCore.Components;

namespace QuickSnapApp.Canvas.Providers;
public partial class BECanvasProvider : BECanvas, IBECanvasProvider
{
    public async Task<ICanvas2DContextProvider> GetCanvas2DAsync()
    {
        var canvas2DContext = await this.CreateCanvas2DAsync();
        return new Canvas2DContextProvider(canvas2DContext);
    }

    public ElementReference GetCanvasReference() => this.CanvasReference;
}
