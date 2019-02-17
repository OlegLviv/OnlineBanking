using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.Deposit;
using OnlineBanking.DAL;

namespace OnlineBanking.BLL.Services
{
    public class DepositService : IDepositService
    {
        private readonly IRepository<DepositType> _depositTypeRepository;
        private readonly IRepository<Deposit> _depositRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public DepositService(IRepository<DepositType> depositTypeRepository,
            IRepository<Deposit> depositRepository,
            IMapper mapper,
            IRepository<User> userRepository)
        {
            _depositTypeRepository = depositTypeRepository;
            _depositRepository = depositRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<DataHolder<DepositDto>> CreateDepositAsync(CreateDepositDto depositDto, Guid userId)
        {
            var depositType = await _depositTypeRepository.GetByIdAsync(depositDto.DepositTypeId);

            if (depositType == null)
                return DataHolder<DepositDto>.CreateFailure("Deposit doesn't exist");

            if (!await CanUserOpenDepositAsync(userId, depositDto.DepositTypeId))
                return DataHolder<DepositDto>.CreateFailure("Can't open deposit");

            var deposit = new Deposit
            {
                UserId = userId,
                DepositType = depositType,
                DepositTypeId = depositType.Id,
                Expire = DateTime.UtcNow.AddMonths(depositType.Months)
            };

            depositType.Deposits.Add(deposit);

            var insertResult = await _depositTypeRepository.UpdateAsync(depositType);

            if (insertResult <= 0)
                return DataHolder<DepositDto>.CreateFailure("Can't create deposit");

            return DataHolder<DepositDto>.CreateSuccess(_mapper.Map<Deposit, DepositDto>(deposit));
        }

        public async Task<DataHolder<ICollection<DepositTypeDto>>> GetDepositTypesAsync(string currency, Guid userId)
        {
            var depositTypes = await _depositTypeRepository
                .Table
                .Where(type => type.Currency == currency)
                .Select(type => new DepositTypeDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    Currency = type.Currency,
                    Months = type.Months,
                    Percentages = type.Percentages,
                    IsTaken = type.Deposits.Any(deposit => deposit.UserId == userId)
                })
                .ToListAsync();

            return DataHolder<ICollection<DepositTypeDto>>.CreateSuccess(depositTypes);
        }

        public async Task<DataHolder<ICollection<DepositDto>>> GetDepositsAsync(Guid userId)
        {
            var deposits = await _depositRepository
                .Table
                .Where(deposit => deposit.UserId == userId)
                .ToListAsync();

            return DataHolder<ICollection<DepositDto>>.CreateSuccess(_mapper.Map<List<Deposit>, List<DepositDto>>(deposits));
        }


        private async Task<bool> CanUserOpenDepositAsync(Guid userId, Guid depositTypeId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return false;

            return user
                .Deposits
                .All(deposit => deposit.DepositTypeId != depositTypeId);
        }
    }
}
