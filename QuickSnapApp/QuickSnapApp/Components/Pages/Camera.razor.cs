using QuickSnappApp.Providers;
using Microsoft.AspNetCore.Components;

namespace QuickSnappApp.Components.Pages;
public sealed partial class Camera : ComponentBase
{
    [Inject]
    private IPermissionsProvider _permissionsProvider { get; init; } = default!;

    [Inject]
    private IMediaPicker _mediaPicker { get; init; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            var currentStatus = await _permissionsProvider.GetCameraPermissionsAsync();

            if (currentStatus != PermissionStatus.Granted)
            {
                var status = await _permissionsProvider.RequestCameraPermissionsAsync();
            }

            var file = await _mediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "thing" });

            if (file != null)
            {
                var stream = await file.OpenReadAsync();
                //FacePhoto = $"~{file.FullPath}";
                //var img = ImageSource.FromStream(() => stream);
                //var bytes = Convert.ToBase64String(stream);
                //var imgUrl =             
            }
        }
        catch (Exception ex)
        {
            var a = ex;
        }


    }
}
