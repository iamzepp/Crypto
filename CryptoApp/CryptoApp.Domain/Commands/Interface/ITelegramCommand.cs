using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoApp.Domain.Commands.Interface
{
    public interface ITelegramCommand
    {
        string Name { get; }
        
        string Description { get; }

        Task<Message> Execute(Message message, ITelegramBotClient client);

        bool Contains(Message message);
    }
}