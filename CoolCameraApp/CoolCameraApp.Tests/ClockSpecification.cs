using Bunit;
using CoolCameraApp.Components.Pages;
using CoolCameraApp.Providers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoolCameraApp.Tests;

public class ClockSpecification
{
    private Mock<ITaskDelayProvider> _taskDelayProviderMock;
    private Mock<ITaskRunProvider> _taskRunProviderMock;
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
    private Mock<ICancellationTokenProvider> _cancellationTokenProviderMock;

    private DateTime now = new(2020, 01, 02, 03, 04, 05);

    private readonly TestContext testContext;

    public ClockSpecification()
    {
        this.testContext = new TestContext();
        testContext.JSInterop.Mode = JSRuntimeMode.Loose;

        // given I mock out the date time provider
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock.Setup(m => m.Now()).Returns(() => now);

        // given I mock out the task run provider
        _taskRunProviderMock = new Mock<ITaskRunProvider>();
        _taskRunProviderMock
            .Setup(m => m.Run(It.IsAny<Action>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.CompletedTask)
            .Callback<Action, CancellationToken>((action, cancellationToken) => action());

        // given I mock out the task delay provider
        _taskDelayProviderMock = new Mock<ITaskDelayProvider>();

        // given I mock out the cancellation token provider
        _cancellationTokenProviderMock = new Mock<ICancellationTokenProvider>();
        _cancellationTokenProviderMock
            .SetupSequence(m => m.IsCancellationRequested(It.IsAny<CancellationTokenSource>()))
            .Returns(() => false)
            .Returns(() => true);

        var provider = new DependencyProvider(testContext.Services);
        provider.Register();

        // given I replace all the mocks.
        ReplaceMocks();
    }

    private void ReplaceMocks()
    {
        testContext.Services.RemoveAll<ITaskDelayProvider>();
        testContext.Services.RemoveAll<ITaskRunProvider>();
        testContext.Services.RemoveAll<IDateTimeProvider>();
        testContext.Services.RemoveAll<ICancellationTokenProvider>();

        testContext.Services.AddSingleton(_taskDelayProviderMock.Object);
        testContext.Services.AddSingleton(_taskRunProviderMock.Object);
        testContext.Services.AddSingleton(_dateTimeProviderMock.Object);
        testContext.Services.AddSingleton(_cancellationTokenProviderMock.Object);
    }

    [Fact]
    public void Test1()
    {
        var component = this.testContext.RenderComponent<Clock>();

        var a = component.Markup;
    }
}