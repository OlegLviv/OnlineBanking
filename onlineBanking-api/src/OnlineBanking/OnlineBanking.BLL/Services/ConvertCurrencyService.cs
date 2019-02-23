using System.Threading.Tasks;
using OnlineBanking.BLL.Services.Abstract;

namespace OnlineBanking.BLL.Services
{
    public class ConvertCurrencyService : IConvertCurrencyService
    {
        //  Todo need to implement in future
        public Task<decimal> ConvertAsync(string currency, decimal amount)
            => Task.FromResult(amount);
    }
}
