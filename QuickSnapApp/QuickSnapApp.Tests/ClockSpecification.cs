using Bunit;
using Bunit.TestDoubles;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using QuickSnapApp.Canvas;
using QuickSnapApp.Components.Pages;
using QuickSnapApp.Providers;

namespace QuickSnapApp.Tests;

public class ClockSpecification
{
    private Mock<ITaskDelayProvider> _taskDelayProviderMock;
    private Mock<ITaskRunProvider> _taskRunProviderMock;
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
    private Mock<ICancellationTokenSourceProvider> _cancellationTokenSourceProviderMock;

    private DateTime now = new(2020, 01, 02, 03, 04, 05);
    private readonly CancellationToken cancellationToken = new();

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
        _cancellationTokenSourceProviderMock = new Mock<ICancellationTokenSourceProvider>();
        _cancellationTokenSourceProviderMock
            .SetupSequence(m => m.IsCancellationRequested())
            .Returns(() => false)
            .Returns(() => true);

        _cancellationTokenSourceProviderMock
            .Setup(m => m.Token())
            .Returns(() => cancellationToken);

        // Given I mock out the analog clock
        testContext.ComponentFactories.AddStub<AnalogClock>();

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
        testContext.Services.RemoveAll<ICancellationTokenSourceProvider>();

        testContext.Services.AddSingleton(_taskDelayProviderMock.Object);
        testContext.Services.AddSingleton(_taskRunProviderMock.Object);
        testContext.Services.AddSingleton(_dateTimeProviderMock.Object);
        testContext.Services.AddSingleton(_cancellationTokenSourceProviderMock.Object);
    }

    [Fact(DisplayName = "Clock sets up a rendering task and delays with cancellation correctly.")]
    public void Test1()
    {
        // When the clock page is rendered
        var component = this.testContext.RenderComponent<Clock>();

        // Then I expect to get the current time
        _dateTimeProviderMock.Verify(m => m.Now(), Times.AtLeastOnce);

        // Then I expect a new ongoing render task created
        _taskRunProviderMock.Verify(m => m.Run(It.IsAny<Action>(), It.IsAny<CancellationToken>()), Times.Once);

        // Then I expect to check if the task is canceled
        _cancellationTokenSourceProviderMock.Verify(m => m.IsCancellationRequested(), Times.AtLeastOnce);

        // Then I expect to get the cancellation token
        _cancellationTokenSourceProviderMock.Verify(m => m.Token(), Times.AtLeastOnce);

        // Then I expect to delay up to a second
        _taskDelayProviderMock.Verify(m => m.Delay(1000, cancellationToken), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Clock sets the analog clock parameters")]
    public void Test2()
    {
        // When the clock page is rendered
        var component = this.testContext.RenderComponent<Clock>();

        // Then I expect the analog clock width to be the following
        var analogClock = component.FindComponent<Stub<AnalogClock>>();
        analogClock.Instance.Parameters.Get(x => x.Width).Should().Be(400);

        // Then I expect the analog clock background color to be the following
        analogClock.Instance.Parameters.Get(x => x.BackgroundColor).Should().Be("#007bff");

        // Then I expect the analog clock foreground color to be the following
        analogClock.Instance.Parameters.Get(x => x.ForegroundColor).Should().Be("#f8f9fa");

        // Then I expect the analog clock time to be the following
        analogClock.Instance.Parameters.Get(x => x.Time).Should().Be(now);
    }

    [Fact(DisplayName = "Clock renders the current time on the digital clock")]
    public void Test3()
    {
        // When the clock page is rendered
        var component = this.testContext.RenderComponent<Clock>();

        // Then I expect the correct time reflected
        component.Markup.Should().Contain("3:04:05 AM");
    }
}