using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.Deposit;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface IDepositService
    {
        Task<DataHolder<DepositDto>> CreateDepositAsync(CreateDepositDto depositDto, Guid userId);

        Task<DataHolder<ICollection<DepositTypeDto>>> GetDepositTypesAsync(string currency, Guid userId);
    }
}
