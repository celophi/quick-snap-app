namespace QuickSnapApp.Providers;
public interface IDateTimeProvider
{
    /// <summary>
    /// Returns the current local date time.
    /// </summary>
    /// <returns></returns>
    DateTime Now();
}
