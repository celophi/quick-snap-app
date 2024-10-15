using Microsoft.AspNetCore.Components;
using QuickSnapApp.Providers;
using System.ComponentModel.DataAnnotations;

namespace QuickSnapApp.Components.Pages;
public sealed partial class Register : ComponentBase
{
    [Inject]
    private IDeviceInfoProvider _deviceInfoProvider { get; init; } = default!;

    private LoginModel loginModel = new LoginModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var name = _deviceInfoProvider.Name();
        //var Model = _deviceInfoProvider.Model();
        var Manufacturer = _deviceInfoProvider.Manufacturer();
        //var Platform = _deviceInfoProvider.Platform();
        //var VersionString = _deviceInfoProvider.VersionString();
        //var IdioType = _deviceInfoProvider.IdioType();
    }

    private void HandleLogin()
    {
        // Add logic to handle login, e.g., calling an API
        //Console.WriteLine($"Username: {loginModel.Username}, Password: {loginModel.Password}");
        // Redirect or show a success message



    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
