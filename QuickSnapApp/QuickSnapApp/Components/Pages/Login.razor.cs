using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace QuickSnapApp.Components.Pages;
public sealed partial class Login : ComponentBase
{
    private LoginModel loginModel = new LoginModel();

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
