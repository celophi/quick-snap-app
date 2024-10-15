
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuickSnapApp.Configuration;
using System.Reflection;

namespace QuickSnapApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitCamera()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddHttpClient();
        builder.AddAppSettings();

        builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiOptions"));

        var envString = builder.Configuration.GetValue<string>("Environment");
        Enum.TryParse(envString, true, out EnvironmentType environmentType);

        if (environmentType == EnvironmentType.Development)
        {
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
        }

        new DependencyProvider(builder.Services).Register();

        return builder.Build();
    }

    /// <summary>
    /// Adds the application settings for configuration.
    /// Application settings files are replaced by the build and publishing pipeline.
    /// For environments other than development, the file 'appsettings.json' is replaced by the environment specific file.
    /// </summary>
    /// <param name="builder"></param>
    private static void AddAppSettings(this MauiAppBuilder builder)
    {
        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("QuickSnapApp.appsettings.json");

        var configurationRoot = new ConfigurationBuilder().AddJsonStream(stream!).Build();
        builder.Configuration.AddConfiguration(configurationRoot);
    }
}
