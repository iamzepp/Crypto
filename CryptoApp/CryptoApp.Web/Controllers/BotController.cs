using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.Domain.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoApp.Web.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ICommandService _commandService;
        private readonly IMainDbConnection _connection;

        public BotController(ICommandService commandService, ITelegramBotClient telegramBotClient, IMainDbConnection connection)
        {
            _commandService = commandService;
            _telegramBotClient = telegramBotClient;
            _connection = connection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null)
            {
                return Ok();
            }

            if (update.CallbackQuery is not null)
            {
                
            }
            

            var message = update.Message;

            foreach (var command in _commandService.Get())
            {
                if (command.Contains(message))
                {
                    command.Connection = _connection; 
                    await command.Execute(message, _telegramBotClient);
                    break;
                }
            }

            return Ok();
        }
    }
}