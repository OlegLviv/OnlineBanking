using System;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.Deposit;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface IDepositService
    {
        Task<DataHolder<Deposit>> CreateDepositAsync(CreateDepositDto depositDto, Guid userId);
    }
}
