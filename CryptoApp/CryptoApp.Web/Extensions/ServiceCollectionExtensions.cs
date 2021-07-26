using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace CryptoApp.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var token = 
                Environment.GetEnvironmentVariable("TELEGRAM_API_TOKEN", EnvironmentVariableTarget.Machine) 
                        // ReSharper disable once NotResolvedInText
                        ?? throw new ArgumentNullException("token");

            var client = new TelegramBotClient(token);
            var webHook = $"{configuration["Url"]}/api/message/update";
            client.SetWebhookAsync(webHook).Wait();
            
            return serviceCollection.AddSingleton<ITelegramBotClient>(_=> client);
        }
    }
}