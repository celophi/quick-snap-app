using QuickSnapApp.Components.Pages;

namespace QuickSnapApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Define Routes
        Routing.RegisterRoute("toolkit-camera", typeof(ToolkitCamera));
    }
}
