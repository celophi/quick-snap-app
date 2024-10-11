﻿namespace QuickSnapApp.Providers;
public sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTime Now() => DateTime.Now;
}
