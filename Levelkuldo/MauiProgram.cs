using CommunityToolkit.Maui;
using Levelkuldo.Services;
using Levelkuldo.ViewModels;
using Levelkuldo.Views;

namespace Levelkuldo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/get-started?tabs=CommunityToolkitMaui
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<FileDialogService>();
        builder.Services.AddSingleton<EmailService>();

        builder.Services.AddTransient<UzenetViewModel>();
        builder.Services.AddSingleton<UzenetPage>();

        builder.Services.AddTransient<NaploViewModel>();
        builder.Services.AddSingleton<NaploPage>();

        return builder.Build();
    }
}
