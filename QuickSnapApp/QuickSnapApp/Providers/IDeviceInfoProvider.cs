namespace QuickSnapApp.Providers;
public interface IDeviceInfoProvider
{
    string Name();
    string Model();
    string Manufacturer();
    DevicePlatform Platform();
    string VersionString();
    string IdioType();
}
