namespace CoolCameraApp.Canvas.Providers;
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTime Now() => DateTime.Now;
}
