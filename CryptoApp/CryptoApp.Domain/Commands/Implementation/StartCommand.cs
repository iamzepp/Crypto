﻿using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.DataAccess.Handlers.Query;
using CryptoApp.Domain.Commands.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class StartCommand : ITelegramCommand
    {
        public string Name => @"/start";
        
        public string Description => "Начать работу с ботом";
        
        public IMainDbConnection Connection { get; set; }

        private IQueryHandler<string, User> _queryHandler => new AuthorizationHandler();
        
        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var result = await _queryHandler.Handle(message.From, Connection);

            return await client.SendTextMessageAsync(chatId, result, ParseMode.Markdown);
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