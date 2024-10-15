namespace QuickSnapApp.Accounts;

public sealed record AccountsRegisterRequest
{
    public required string Name { get; init; }
    public required string Password { get; init; }
    public required string DeviceName { get; init; }
    public required string DeviceManufacturer { get; init; }
}
