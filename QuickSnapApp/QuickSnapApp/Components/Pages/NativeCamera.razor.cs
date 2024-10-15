using Microsoft.AspNetCore.Components;
using QuickSnapApp.Pictures;
using QuickSnapApp.Providers;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Pages;
public sealed partial class NativeCamera : ComponentBase
{
    [Inject]
    private IPermissionsProvider _permissionsProvider { get; init; } = default!;

    [Inject]
    private IMediaPicker _mediaPicker { get; init; } = default!;

    [Inject]
    private IPicturesProvider _picturesProvider { get; init; } = default!;

    [Inject]
    private ISecureStorageProvider _secureStorageProvider { get; init; } = default!;

    [Inject]
    private INavigationService _navigationService { get; init; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var currentStatus = await _permissionsProvider.GetCameraPermissionsAsync();

        if (currentStatus != PermissionStatus.Granted)
        {
            var status = await _permissionsProvider.RequestCameraPermissionsAsync();
        }

        var file = await _mediaPicker.CapturePhotoAsync();

        if (file != null)
        {
            var stream = await file.OpenReadAsync();
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, default);

            var token = await _secureStorageProvider.GetAsync("authToken");
            await _picturesProvider.SubmitAsync(token!, new PicturesSubmitRequestViewModel { Data = buffer, ContentType = file.ContentType });
        }

        _navigationService.NavigateToBlazor("/home");
    }
}
