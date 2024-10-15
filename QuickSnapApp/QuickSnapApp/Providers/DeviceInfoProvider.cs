namespace QuickSnapApp.Providers;
public sealed class DeviceInfoProvider : IDeviceInfoProvider
{
    public string Name() => DeviceInfo.Name;
    public string Model() => DeviceInfo.Model;
    public string Manufacturer() => DeviceInfo.Manufacturer;
    public DevicePlatform Platform() => DeviceInfo.Platform;
    public string VersionString() => DeviceInfo.VersionString;
    public string IdioType() => DeviceInfo.Idiom.ToString();
}
