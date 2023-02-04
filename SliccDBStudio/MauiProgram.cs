using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SliccDBStudio.Data;
using MudBlazor.Services;
using SliccDBStudio.Services;
using VisNetwork.Blazor;
using BlazorTextEditor.RazorLib;


namespace SliccDBStudio;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services.AddBlazorTextEditor();
        builder.Services.AddMudServices();
        builder.Services.AddSingleton<DatabaseConnectionService>();
        builder.Services.AddSingleton<GraphDisplayService>();
        builder.Services.AddVisNetwork();

        return builder.Build();
	}
}
