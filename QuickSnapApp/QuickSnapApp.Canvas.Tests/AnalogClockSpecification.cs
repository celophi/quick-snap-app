using Blazor.Extensions.Canvas.Canvas2D;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using QuickSnapApp.Canvas.Providers;

namespace QuickSnapApp.Canvas.Tests;

public class AnalogClockSpecification
{
    private readonly Mock<BECanvasProvider> _stagingCanvasMock;
    private readonly Mock<BECanvasProvider> _targetCanvasMock;
    private readonly Mock<ICanvas2DContextProvider> _stagingContextMock;
    private readonly Mock<ICanvas2DContextProvider> _targetContextMock;
    private readonly Mock<IMathProvider> _mathProviderMock;

    private readonly TestContext testContext;

    private DateTime currentTime = new(2000, 01, 02, 12, 0, 0);
    private const int Width = 100;
    private const string BackgroundColor = "pink";
    private const string ForegroundColor = "yellow";

    public AnalogClockSpecification()
    {
        this.testContext = new TestContext();
        testContext.JSInterop.Mode = JSRuntimeMode.Loose;

        // given I mock out PI
        _mathProviderMock = new Mock<IMathProvider>();
        _mathProviderMock.Setup(m => m.PI).Returns(() => 6);

        // given I mock out the staging context
        _stagingContextMock = new Mock<ICanvas2DContextProvider>();

        // given I mock out the target context
        _targetContextMock = new Mock<ICanvas2DContextProvider>();

        // given I mock out the staging canvas
        _stagingCanvasMock = new Mock<BECanvasProvider>();
        _stagingCanvasMock
            .Setup(m => m.GetCanvas2DAsync())
            .ReturnsAsync(() => _stagingContextMock.Object);

        // given I mock out the target canvas
        _targetCanvasMock = new Mock<BECanvasProvider>();
        _targetCanvasMock
            .Setup(m => m.GetCanvas2DAsync())
            .ReturnsAsync(() => _targetContextMock.Object);

        // given I mock out the canvas factory.
        int invocationCount = 0;
        testContext.ComponentFactories.AddMock(() =>
        {
            invocationCount++;
            return invocationCount switch
            {
                1 => _stagingCanvasMock.Object,
                2 => _targetCanvasMock.Object,
                _ => new Mock<BECanvasProvider>().Object
            };
        });

        // given I replace all the mocks.
        ReplaceMocks();
    }

    private void ReplaceMocks()
    {
        testContext.Services.RemoveAll<IMathProvider>();
        testContext.Services.AddSingleton(_mathProviderMock.Object);
    }

    [Fact(DisplayName = "Analog clock time parameter set should draw the clock face, hands, time in order with the correct math.")]
    public void Test1()
    {
        // Given I have the following drawing order expectation and math
        _stagingContextMock.ShouldBeInOrder(

            // DrawAsync
            m => m.BeginBatchAsync(),
            m => m.SetTransformAsync(1, 0, 0, 1, 0, 0),
            m => m.SetFillStyleAsync("white"),
            m => m.FillRectAsync(0, 0, Width, Width),
            m => m.TranslateAsync(50, 50),
            m => m.ArcAsync(0, 0, 45f, 0, 12, null),
            m => m.SetFillStyleAsync(BackgroundColor),
            m => m.FillAsync(),

            // DrawFaceAsync
            m => m.BeginPathAsync(),
            m => m.ArcAsync(0, 0, 45f, 0, 12, null),
            m => m.SetFillStyleAsync(BackgroundColor),
            m => m.FillAsync(),
            m => m.BeginPathAsync(),
            m => m.ArcAsync(0, 0, 4.5f, 0, 12, null),
            m => m.SetFillStyleAsync(ForegroundColor),
            m => m.FillAsync(),

            // DrawNumbers
            m => m.SetFillStyleAsync("white"),
            m => m.SetFontAsync("6.75px arial"),
            m => m.SetTextBaselineAsync(TextBaseline.Middle),
            m => m.SetTextAlignAsync(TextAlign.Center),

            // DrawNumbers 1
            m => m.RotateAsync(1),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-1),
            m => m.FillTextAsync("1", 0, 0, null),
            m => m.RotateAsync(1),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-1),

            // DrawNumbers 2
            m => m.RotateAsync(2),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-2),
            m => m.FillTextAsync("2", 0, 0, null),
            m => m.RotateAsync(2),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-2),

            // DrawNumbers 3
            m => m.RotateAsync(3),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-3),
            m => m.FillTextAsync("3", 0, 0, null),
            m => m.RotateAsync(3),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-3),

            // DrawNumbers 4
            m => m.RotateAsync(4),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-4),
            m => m.FillTextAsync("4", 0, 0, null),
            m => m.RotateAsync(4),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-4),

            // DrawNumbers 5
            m => m.RotateAsync(5),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-5),
            m => m.FillTextAsync("5", 0, 0, null),
            m => m.RotateAsync(5),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-5),

            // DrawNumbers 6
            m => m.RotateAsync(6),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-6),
            m => m.FillTextAsync("6", 0, 0, null),
            m => m.RotateAsync(6),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-6),

            // DrawNumbers 7
            m => m.RotateAsync(7),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-7),
            m => m.FillTextAsync("7", 0, 0, null),
            m => m.RotateAsync(7),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-7),

            // DrawNumbers 8
            m => m.RotateAsync(8),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-8),
            m => m.FillTextAsync("8", 0, 0, null),
            m => m.RotateAsync(8),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-8),

            // DrawNumbers 9
            m => m.RotateAsync(9),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-9),
            m => m.FillTextAsync("9", 0, 0, null),
            m => m.RotateAsync(9),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-9),

            // DrawNumbers 10
            m => m.RotateAsync(10),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-10),
            m => m.FillTextAsync("10", 0, 0, null),
            m => m.RotateAsync(10),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-10),

            // DrawNumbers 11
            m => m.RotateAsync(11),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-11),
            m => m.FillTextAsync("11", 0, 0, null),
            m => m.RotateAsync(11),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-11),

            // DrawNumbers 12
            m => m.RotateAsync(12),
            m => m.TranslateAsync(0, -38.25),
            m => m.RotateAsync(-12),
            m => m.FillTextAsync("12", 0, 0, null),
            m => m.RotateAsync(12),
            m => m.TranslateAsync(0, 38.25),
            m => m.RotateAsync(-12),

            // Draw Hands (hour)
            m => m.SetStrokeStyleAsync(ForegroundColor),
            m => m.BeginPathAsync(),
            m => m.SetLineWidthAsync(45f * 0.07f),
            m => m.SetLineCapAsync(LineCap.Round),
            m => m.MoveToAsync(0, 0),
            m => m.RotateAsync(0),
            m => m.LineToAsync(0, -22.5),
            m => m.StrokeAsync(),
            m => m.RotateAsync(0),

            // Draw Hands (minute)
            m => m.SetStrokeStyleAsync(ForegroundColor),
            m => m.BeginPathAsync(),
            m => m.SetLineWidthAsync(45f * 0.07f),
            m => m.SetLineCapAsync(LineCap.Round),
            m => m.MoveToAsync(0, 0),
            m => m.RotateAsync(0),
            m => m.LineToAsync(0, -36),
            m => m.StrokeAsync(),
            m => m.RotateAsync(0),

            // Draw Hands (second)
            m => m.SetStrokeStyleAsync(ForegroundColor),
            m => m.BeginPathAsync(),
            m => m.SetLineWidthAsync(45f * 0.02f),
            m => m.SetLineCapAsync(LineCap.Round),
            m => m.MoveToAsync(0, 0),
            m => m.RotateAsync(0),
            m => m.LineToAsync(0, -40.5),
            m => m.StrokeAsync(),
            m => m.RotateAsync(0),

            // Finish drawing
            m => m.EndBatchAsync()
        );

        // Given I render and set parameters.
        var component = this.testContext.RenderComponent<AnalogClock>(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
        );

        // When I set the time parameter
        var action = () => component.SetParametersAndRender(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
          .Add(p => p.Time, currentTime)
        );

        // Then I expect no out of order commands
        action.Should().NotThrow();
    }

    [Fact(DisplayName = "Analog clock time parameter set should setup the double buffer canvases and contexts.")]
    public void Test2()
    {
        // Given I render and set parameters.
        var component = this.testContext.RenderComponent<AnalogClock>(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
        );

        // When I set the time parameter
        component.SetParametersAndRender(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
          .Add(p => p.Time, currentTime)
        );

        // Then I expect to have a staging context
        _stagingCanvasMock.Verify(m => m.GetCanvas2DAsync(), Times.Once);

        // Then I expect to have a target context
        _targetCanvasMock.Verify(m => m.GetCanvas2DAsync(), Times.Once);
    }

    [Fact(DisplayName = "Analog clock time parameter set should draw the staging canvas to the target canvas.")]
    public void Test3()
    {
        // Given I render and set parameters.
        var component = this.testContext.RenderComponent<AnalogClock>(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
        );

        // When I set the time parameter
        component.SetParametersAndRender(parameters => parameters
          .Add(p => p.Width, Width)
          .Add(p => p.BackgroundColor, BackgroundColor)
          .Add(p => p.ForegroundColor, ForegroundColor)
          .Add(p => p.Time, currentTime)
        );

        // Then I expect to copy to the target canvas
        _targetContextMock.Verify(m => m.DrawImageAsync(_stagingCanvasMock.Object.GetCanvasReference(), 0, 0), Times.Once);
    }
}