using CombatTracker.ViewModel;

namespace CombatTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        // Singleton -> global one copy only
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        // Transient -> create and destroy every time
        builder.Services.AddTransient<CreaNuovoPage>();
        builder.Services.AddTransient<CreaNuovoViewModel>();

        builder.Services.AddTransient<EditPage>();
        builder.Services.AddTransient<EditViewModel>();

        builder.Services.AddTransient<ImportaPartyPage>();
        builder.Services.AddTransient<ImportaPartyViewModel>();

        builder.Services.AddTransient<ModificaPfPage>();
        builder.Services.AddTransient<ModificaPfViewModel>();

        builder.Services.AddTransient<SalvaPartyPage>();
        builder.Services.AddTransient<SalvaPartyViewModel>();
        return builder.Build();
    }
}
