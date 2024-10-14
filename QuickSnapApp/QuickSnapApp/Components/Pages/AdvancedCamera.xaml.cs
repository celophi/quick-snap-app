using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using QuickSnapApp.Services;

namespace QuickSnapApp.Components.Pages;

public partial class AdvancedCamera : ContentPage
{
    private INavigationService _navigationService;
    private ICameraProvider _cameraProvider;

    public AdvancedCamera(INavigationService navigationService, ICameraProvider cameraProvider)
    {
        InitializeComponent();
        _navigationService = navigationService;
        _cameraProvider = cameraProvider;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        DefaultCamera!.SelectedCamera = _cameraProvider.AvailableCameras
            .Where(c => c.Position == CameraPosition.Front)
            .FirstOrDefault();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

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
}