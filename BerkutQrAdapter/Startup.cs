using System;
using Azure.Identity;
using BerkutQrAdapter.Infrastructure;
using BerkutQrAdapter.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(BerkutQrAdapter.Startup))]
namespace BerkutQrAdapter
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<ServiceBusOptions>(_functionConfig.GetSection("ServiceBusOptions"));
            builder.Services.Configure<BotOptions>(_functionConfig.GetSection("BotOptions"));

            builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
                new TelegramBotClient(provider.GetService<IOptions<BotOptions>>().Value.Token));
            builder.Services.AddAzureClients(clientBuilder =>
            {
                var provider = builder.Services.BuildServiceProvider();

                clientBuilder.UseCredential(new DefaultAzureCredential());
                clientBuilder.AddServiceBusClientWithNamespace(provider.GetRequiredService<IOptions<ServiceBusOptions>>().Value.FullyQualifiedNamespace);
            });

            builder.Services.AddLogging();
            builder.Services.AddTransient<ITgMessageFactory, TgMessageFactory>();
            builder.Services.AddTransient<IServiceBusMessageFactory, ServiceBusMessageFactory>();
            builder.Services.AddTransient<ITgFileDownloader, TgFileDownloader>();
            builder.Services.AddTransient<IBarcodeDecoder, BarcodeDecoder>();
            builder.Services.AddTransient<IPhotoManager, PhotoManager>();
        }
    }
}

