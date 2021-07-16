using System.Threading.Tasks;
using CryptoApp.Domain.Commands.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class HelpCommand : ITelegramCommand
    {
        public string Name => "Help";
        
        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            return await client.SendTextMessageAsync(chatId, Name);
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