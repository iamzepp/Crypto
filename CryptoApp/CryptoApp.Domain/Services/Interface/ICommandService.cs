using System.Collections.Generic;
using CryptoApp.Domain.Commands.Interface;

namespace CryptoApp.Domain.Services.Interface
{
    public interface ICommandService
    {
        List<ITelegramCommand> Get();
    }
}