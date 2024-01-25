using Microsoft.Extensions.Logging;
using MAUIAPP.ViewModels;

namespace MAUIAPP;

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
		builder.Services.AddSingleton<MedicineCollectionViewModel>();
		builder.Services.AddSingleton<ProducerCollectionViewModel>();
		builder.Services.AddSingleton<MedicinePage>();
		builder.Services.AddSingleton<ProducerPage>();
        builder.Services.AddSingleton<EditMedicinePage>();
		builder.Services.AddSingleton<AddNewMedicinePage>();
		builder.Services.AddSingleton<AddNewProducerPage>();
		builder.Services.AddSingleton<EditProducerPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

