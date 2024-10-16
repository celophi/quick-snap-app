using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using QuickSnapApp.Pictures;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Pages;

public partial class ToolkitCamera : ContentPage
{
    private readonly INavigationService _navigationService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IPictureRepository _pictureRepository;

    public ToolkitCamera(INavigationService navigationService, ICameraProvider cameraProvider, IPictureRepository pictureRepository)
    {
        InitializeComponent();
        _navigationService = navigationService;
        _cameraProvider = cameraProvider;
        _pictureRepository = pictureRepository;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _cameraProvider.RefreshAvailableCameras(default);

        DefaultCamera!.SelectedCamera = _cameraProvider.AvailableCameras
            .Where(c => c.Position == CameraPosition.Front)
            .FirstOrDefault();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        DefaultCamera.Handler?.DisconnectHandler();
    }

    /// <summary>
    /// Button handler for the cancel event.
    /// Navigates to the root view.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCancel(object sender, EventArgs e)
    {
        _navigationService.NavigateFromXamlToBlazor("/");
    }

    /// <summary>
    /// Button handler for the capture event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnCapture(object sender, EventArgs e)
    {
        await DefaultCamera!.CaptureImage(default);
    }

    /// <summary>
    /// Button handler for the switch camera event.
    /// Switches the selected camera based on position.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSwitch(object sender, EventArgs e)
    {
        DefaultCamera!.SelectedCamera = _cameraProvider.AvailableCameras!
            .Where(c => c.Position != DefaultCamera!.SelectedCamera.Position)
            .FirstOrDefault();
    }

    private async void OnMediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
    {
        var buffer = new byte[e.Media.Length];
        await e.Media.ReadAsync(buffer, default);

        await _pictureRepository.SaveAsync(buffer, "image/png");

        await _navigationService.NavigateFromXamlToBlazor("/home");
    }
}