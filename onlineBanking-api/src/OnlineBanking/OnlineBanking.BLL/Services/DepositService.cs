using System;
using System.Threading.Tasks;
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

        public DepositService(IRepository<DepositType> depositTypeRepository, IRepository<Deposit> depositRepository)
        {
            _depositTypeRepository = depositTypeRepository;
            _depositRepository = depositRepository;
        }

        public async Task<DataHolder<Deposit>> CreateDepositAsync(CreateDepositDto depositDto, Guid userId)
        {
            var depositType = await _depositTypeRepository.GetByIdAsync(depositDto.DepositTypeId);

            if (depositType == null)
                return DataHolder<Deposit>.CreateFailure("Deposit don't exist");

            if (!await CanUserOpenDepositAsync(userId))
                return DataHolder<Deposit>.CreateFailure("Can't open deposit");

            var deposit = new Deposit
            {
                UserId = userId,
                DepositType = depositType,
                DepositTypeId = depositType.Id,
                Expire = DateTime.UtcNow.AddMonths(depositType.Months)
            };

            var insertResult = await _depositRepository.InsertAsync(deposit);

            if (insertResult <= 0)
                return DataHolder<Deposit>.CreateFailure("Can't create deposit");

            return DataHolder<Deposit>.CreateSuccess(deposit);
        }

        //  Todo: will be add in future
        private static Task<bool> CanUserOpenDepositAsync(Guid userId)
            => Task.FromResult(true);
    }
}
