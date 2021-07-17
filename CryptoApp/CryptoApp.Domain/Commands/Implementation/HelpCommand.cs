using System.Text;
using System.Threading.Tasks;
using CryptoApp.Domain.Commands.Interface;
using CryptoApp.Domain.Services.Implementation;
using CryptoApp.Domain.Services.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class HelpCommand : ITelegramCommand
    {
        public string Name => @"/help";
        
        public string Description => "Получить список команд";
        
        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        { 
            var chatId = message.Chat.Id;

            ICommandService service = new CommandService();

            var strBuilder = new StringBuilder();
            strBuilder.Append("Список комманд:\n");

            foreach (var command in service.Get())
            {
                strBuilder.Append($"{command.Name} - {command.Description}\n");
            }
            
            return await client.SendTextMessageAsync(chatId, 
                strBuilder.ToString(), ParseMode.Markdown);
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