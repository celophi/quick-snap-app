namespace QuickSnapApp.Providers;

public sealed class PermissionsProvider : IPermissionsProvider
{
    /// <inheritdoc/>
    public async Task<PermissionStatus> GetCameraPermissionsAsync() =>
        await Permissions.CheckStatusAsync<Permissions.Camera>();

    /// <inheritdoc/>
    public async Task<PermissionStatus> RequestCameraPermissionsAsync() =>
        await Permissions.RequestAsync<Permissions.Camera>();
}