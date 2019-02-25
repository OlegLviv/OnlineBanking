using System;
using System.Threading.Tasks;
using OnlineBanking.Core;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.CreditCard;
using OnlineBanking.Core.Models.Dtos.Logs;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface ICreditCardService
    {
        Task<DataHolder<CreditCardOrder>> CreateOrderAsync(CreateCreditCardOrderDto dto, User user);

        Task<DataHolder<CreditCard>> GetById(Guid id, Guid userId);

        Task<DataHolder<CreditCard>> ChangePinAsync(ChangePinDto pinDto, Guid userId);

        Task<DataHolder<CreditCard>> ChangeCreditLimitAsync(ChangeCreditLimitDto limitDto, Guid userId);

        Task<DataHolder<CreditCard>> SendMoneyAsync(SendMoneyDto sendMoneyDto, Guid userId);

        Task<DataHolder<Pager<TransactionMoneyLogDto>>> GetTransactionMoneyLogAsync(Guid cardId, int itemPerPage, int page);
    }
}
