﻿using Microsoft.Extensions.Logging;
using TDMDUAPP.Domain.Model;
using TDMDUAPP.Domain.Services;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<IPreferences>(p => Preferences.Default);
            builder.Services.AddSingleton<IBridgeConnectorHueLights, BridgeConnector>();
            builder.Services.AddSingleton<ILampControl, ViewModel>();

            return builder.Build();
        }
    }
}
