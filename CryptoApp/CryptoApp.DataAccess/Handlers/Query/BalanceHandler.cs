using System.Data;
using System.Threading.Tasks;
using CryptoApp.Models.Models;
using Dapper;

namespace CryptoApp.DataAccess.Handlers.Query
{
    public class BalanceHandler : IQueryHandler<string, OperationModel>
    {
        public async Task<string> Handle(OperationModel request, IDbConnection connection)
        {
            var sqlInsert = 
                @"INSERT INTO domain.account_actions (user_id, crypto_operartion_id, crypto_currency_id, value, description)
                  VALUES (@UserId, @CryptoOperationId, @CryptoCurrencyId, @Value, @Description);";

            using (connection)
            {
                
                await connection.ExecuteAsync(sqlInsert, new
                {
                    UserId = request.UserId,
                    CryptoOperationId = (int)request.Operation,
                    CryptoCurrencyId = (int)request.Currency,
                    Value = request.Value,
                    Description = request.Description
                } );
            }

            return "Успешно добавлено!";
        }
    }
}