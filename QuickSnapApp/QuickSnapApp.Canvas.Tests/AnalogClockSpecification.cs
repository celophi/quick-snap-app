using Blazor.Extensions.Canvas;
using Bunit;
using Moq;

namespace QuickSnapApp.Canvas.Tests;

public class AnalogClockSpecification
{
    private readonly TestContext testContext;

    private readonly Mock<BECanvas> canvasMock;

    public AnalogClockSpecification()
    {
        this.testContext = new TestContext();
        testContext.JSInterop.Mode = JSRuntimeMode.Loose;

        //canvasMock = new Mock<BECanvas>();

        //this.testContext.ComponentFactories.Add(canvasMock.Object, );
    }

    [Fact]
    public void Test1()
    {
        // I don't know how to test this at the moment.
        var component = this.testContext.RenderComponent<AnalogClock>();
        component.InvokeAsync(() => component.Instance.DrawAsync(DateTime.Now));
    }
}