using Microsoft.Extensions.Logging;
using ContainersApp.BLC;
using ContainersMauiApp.ViewModels;
using System.Configuration;

namespace ContainersMauiApp
{
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

            builder.Services.AddSingleton(provider => new BLC(ConfigurationManager.AppSettings["DBLibrary"]));

            builder.Services.AddSingleton<ContainerCollectionViewModel>();
            builder.Services.AddSingleton<ProducerCollectionViewModel>();

            builder.Services.AddSingleton<ContainersPage>();
            builder.Services.AddSingleton<ContainerAddPage>();
            builder.Services.AddSingleton<ContainerEditPage>();

            builder.Services.AddSingleton<ProducersPage>();
            builder.Services.AddSingleton<ProducerAddPage>();
            builder.Services.AddSingleton<ProducerEditPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
