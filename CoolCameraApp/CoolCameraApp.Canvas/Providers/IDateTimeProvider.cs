namespace CoolCameraApp.Canvas.Providers;
internal interface IDateTimeProvider
{
    /// <summary>
    /// Returns the current local date time.
    /// </summary>
    /// <returns></returns>
    DateTime Now();
}
