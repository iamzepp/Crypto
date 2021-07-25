using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.Domain.Commands.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class AddCommand : ITelegramCommand
    {
        public string Name => @"/add";

        public string Description => "Добавить валюту";
        
        public IMainDbConnection Connection { get; set; }

        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            
            var keyboard = new InlineKeyboardMarkup( new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("option1", "/help"), 
                    InlineKeyboardButton.WithCallbackData("option2")  
                }
            });
            

            return await client.SendTextMessageAsync(chatId,"ОК", ParseMode.Markdown, replyMarkup: keyboard);
        }

        public bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
            {
                return false;
            }

            return message.Text != null && message.Text.Contains(Name);
        }
    }
}