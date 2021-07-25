using System.Data;
using System.Threading.Tasks;
using Dapper;
using Telegram.Bot.Types;

namespace CryptoApp.DataAccess.Handlers.Query
{
    public class AuthorizationHandler : IQueryHandler<string, User>
    {
        public async Task<string> Handle(User request, IDbConnection connection)
        {
            var sqlCheck = 
                @"SELECT COUNT(*) FROM auth.users WHERE user_id = @UserId;";
            
            var sqlInsert = 
                @"INSERT INTO auth.users (user_id, user_name, last_name, first_name, is_bot)
                  VALUES (@UserId, @UserName, @LastName, @FirstName, @IsBot);";

            using (connection)
            {
                connection.Open();
                
                var count = await connection.ExecuteScalarAsync<int>(sqlCheck, new { UserId = request.Id });

                if (count != 0)
                {
                    return "Вы были зарегестрированы ранее!";
                }
                
                await connection.ExecuteAsync(sqlInsert, new
                {
                    UserId = request.Id,
                    UserName = request.Username,
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    IsBot = request.IsBot
                } );
            }

            return "Вы зарегистрировались!";
        }
    }
}