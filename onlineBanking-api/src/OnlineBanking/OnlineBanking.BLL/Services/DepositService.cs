using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.Deposit;
using OnlineBanking.DAL;

namespace OnlineBanking.BLL.Services
{
    public class DepositService : IDepositService
    {
        private readonly IRepository<DepositType> _depositTypeRepository;
        private readonly IRepository<Deposit> _depositRepository;
        private readonly IMapper _mapper;

        public DepositService(IRepository<DepositType> depositTypeRepository, IRepository<Deposit> depositRepository, IMapper mapper)
        {
            _depositTypeRepository = depositTypeRepository;
            _depositRepository = depositRepository;
            _mapper = mapper;
        }

        public async Task<DataHolder<DepositDto>> CreateDepositAsync(CreateDepositDto depositDto, Guid userId)
        {
            var depositType = await _depositTypeRepository.GetByIdAsync(depositDto.DepositTypeId);

            if (depositType == null)
                return DataHolder<DepositDto>.CreateFailure("Deposit don't exist");

            if (!await CanUserOpenDepositAsync(userId))
                return DataHolder<DepositDto>.CreateFailure("Can't open deposit");

            var deposit = new Deposit
            {
                UserId = userId,
                DepositType = depositType,
                DepositTypeId = depositType.Id,
                Expire = DateTime.UtcNow.AddMonths(depositType.Months)
            };

            var insertResult = await _depositRepository.InsertAsync(deposit);

            if (insertResult <= 0)
                return DataHolder<DepositDto>.CreateFailure("Can't create deposit");

            return DataHolder<DepositDto>.CreateSuccess(_mapper.Map<Deposit, DepositDto>(deposit));
        }

        public async Task<DataHolder<ICollection<DepositTypeDto>>> GetDepositTypesAsync(string currency)
        {
            var depositTypes = await _depositTypeRepository
                .Table
                .Where(type => type.Currency == currency)
                .ToListAsync();

            return DataHolder<ICollection<DepositTypeDto>>
                .CreateSuccess(_mapper.Map<ICollection<DepositType>, ICollection<DepositTypeDto>>(depositTypes));
        }

        //  Todo: will be add in future
        private static Task<bool> CanUserOpenDepositAsync(Guid userId)
            => Task.FromResult(true);
    }
}
