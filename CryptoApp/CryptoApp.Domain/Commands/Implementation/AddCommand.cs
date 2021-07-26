using System.Linq;
using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.Domain.Commands.Interface;
using CryptoApp.Extensions.Extensions;
using CryptoApp.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class AddCommand : ITelegramCommand
    {
        public string Name => @"/contact";

        public string Description => "Отправить информацию о контакте";
        
        public IMainDbConnection Connection { get; set; }

        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var list = EnumExtensions.GetValues<Currency>()
                .Select(x => new { Name = x.GetDescription(), Id = (int) x })
                .ToList();

            var text = @"Пожалуйста, отправте свой контакт";
            
            var keyboard = new ReplyKeyboardMarkup(
                new[] {
                    new[]{
                        new KeyboardButton("Отправить контакт")
                        {
                            RequestContact = true
                        },
                        new KeyboardButton("Отправить местоположение")
                        {
                            RequestLocation = true
                        }
                    }
                });

            return await client.SendTextMessageAsync(chatId, text, ParseMode.Markdown, replyMarkup: keyboard);
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