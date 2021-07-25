using System.Data;
using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoApp.Domain.Commands.Interface
{
    public interface ITelegramCommand
    {
        IMainDbConnection Connection { get; set; }
        
        string Name { get; }
        
        string Description { get; }

        Task<Message> Execute(Message message, ITelegramBotClient client);

        bool Contains(Message message);
    }
}