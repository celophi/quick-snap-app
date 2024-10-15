using Microsoft.AspNetCore.Components;
using QuickSnapApp.Accounts;
using QuickSnapApp.Services;
using System.ComponentModel.DataAnnotations;

namespace QuickSnapApp.Components.Pages;
public sealed partial class Register : ComponentBase
{
    [Inject]
    private IAccountsRepository _accountsRepository { get; init; } = default!;

    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    private RegisterModel registerModel = new();

    private async Task OnRegister()
    {
        await _accountsRepository.RegisterAsync(registerModel.Username, registerModel.Password);
        _navigationService.NavigateToBlazor("/home");
    }

    private class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
