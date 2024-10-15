namespace QuickSnapApp.API;
public sealed class AccountsRegisterRequestViewModel
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string DeviceName { get; init; }
    public required string DeviceManufacturer { get; init; }
}
