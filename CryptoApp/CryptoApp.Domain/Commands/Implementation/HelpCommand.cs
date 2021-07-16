using System.Threading.Tasks;
using CryptoApp.Domain.Commands.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class HelpCommand : ITelegramCommand
    {
        public string Name => "\U0001F4D6 Помощь";
        
        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var keyboardButtonList = new[]
            {
                new[]
                {
                    new KeyboardButton("\U0001F3E0 Главная")
                },
                new[]
                {
                    new KeyboardButton("\U0001F451 Ранк")
                },
                new[]
                {
                    new KeyboardButton("\U0001F45C Магазин")
                },
                new[]
                {
                    new KeyboardButton("\U0001F4D6 Помощь")
                }
            };
            
            var keyBoard = new ReplyKeyboardMarkup(keyboardButtonList);

           return await client.SendTextMessageAsync(chatId, "\U0001F4D6 Помощь",
                parseMode: ParseMode.Markdown, replyMarkup:keyBoard);
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