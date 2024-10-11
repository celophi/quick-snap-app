namespace QuickSnapApp.Providers;
public interface IPermissionsProvider
{
    /// <summary>
    /// Gets the camera permissions.
    /// </summary>
    /// <returns></returns>
    Task<PermissionStatus> GetCameraPermissionsAsync();

    /// <summary>
    /// Requests access to the camera
    /// </summary>
    /// <returns></returns>
    Task<PermissionStatus> RequestCameraPermissionsAsync();
}
