using System.Threading.Tasks;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.CreditCard;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface ICreditCardService
    {
        Task<DataHolder<CreditCardOrder>> CreateOrderAsync(CreateCreditCardOrderDto dto, User user);
    }
}
