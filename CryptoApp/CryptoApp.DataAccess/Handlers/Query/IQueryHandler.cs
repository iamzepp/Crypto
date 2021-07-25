using System.Data;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess.Handlers.Query
{
    public interface IQueryHandler<TResponse, in TRequest> where TResponse : class
    {
        Task<TResponse> Handle(TRequest request, IDbConnection connection);
    }
}