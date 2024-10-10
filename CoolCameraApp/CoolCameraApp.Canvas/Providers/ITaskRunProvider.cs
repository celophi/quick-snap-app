﻿namespace CoolCameraApp.Canvas.Providers;
internal interface ITaskRunProvider
{
    /// <summary>
    /// Starts a new task.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Run(Action action, CancellationToken cancellationToken = default);
}
