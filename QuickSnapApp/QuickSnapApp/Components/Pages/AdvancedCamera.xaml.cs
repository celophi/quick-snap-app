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

    // Implemented as a follow up video https://youtu.be/JUdfA7nFdWw
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _cameraProvider.RefreshAvailableCameras(CancellationToken.None);
        DefaultCamera!.SelectedCamera = _cameraProvider.AvailableCameras
            .Where(c => c.Position == CameraPosition.Front).FirstOrDefault();
    }

    // Implemented as a follow up video https://youtu.be/JUdfA7nFdWw
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        DefaultCamera.Handler?.DisconnectHandler();
    }


}