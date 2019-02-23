using System.Threading.Tasks;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface IConvertCurrencyService
    {
        Task<decimal> ConvertAsync(string currency, decimal amount);
    }
}
