using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.Logs;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.CreditCard;
using OnlineBanking.DAL;
using OnlineBanking.Core.Extensions.DomainModelExtensions;
using OnlineBanking.Core.Extensions.Linq;
using OnlineBanking.Core.Models.Dtos.Logs;
using OnlineBanking.Core.Models.Dtos.User;
using OnlineBanking.Core.Models.Dtos.User.Register;

namespace OnlineBanking.BLL.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepository<CreditCardOrder> _creditCardOrderRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<CreditCard> _creditCardRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IConvertCurrencyService _convertCurrencyService;
        private readonly IRepository<TransactionMoneyLog> _transactionMoneyLogRepository;

        public CreditCardService(IRepository<CreditCardOrder> creditCardOrderRepository,
            IMapper mapper,
            IRepository<CreditCard> creditCardRepository,
            IRepository<User> userRepository,
            IConvertCurrencyService convertCurrencyService,
            IRepository<TransactionMoneyLog> transactionMoneyLogRepository)
        {
            _creditCardOrderRepository = creditCardOrderRepository;
            _mapper = mapper;
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            _convertCurrencyService = convertCurrencyService;
            _transactionMoneyLogRepository = transactionMoneyLogRepository;
        }

        public async Task<DataHolder<CreditCardOrder>> CreateOrderAsync(CreateCreditCardOrderDto dto, User user)
        {
            var creditCardOrder = _mapper.Map<CreateCreditCardOrderDto, CreditCardOrder>(dto);

            if (creditCardOrder.DeliveryType == CreditCardOrderDeliveryType.Address
                && (string.IsNullOrWhiteSpace(dto.City)
                || string.IsNullOrWhiteSpace(dto.FlatNumber)
                || string.IsNullOrWhiteSpace(dto.HouseNumber)))
                return DataHolder<CreditCardOrder>.CreateFailure("City or flat number order house number is empty");

            creditCardOrder.Status = CreditCardOrderStatus.New;
            creditCardOrder.User = user;

            var insertResult = await _creditCardOrderRepository.InsertAsync(creditCardOrder);

            if (insertResult < 0)
                return DataHolder<CreditCardOrder>.CreateFailure("Cant create order");

            return DataHolder<CreditCardOrder>.CreateSuccess(creditCardOrder);
        }

        public async Task<DataHolder<CreditCard>> GetById(Guid id, Guid userId)
        {
            var creditCard = await GetCreditCardById(id, userId);

            if (creditCard == null)
                return DataHolder<CreditCard>.CreateFailure("Card doesn't exist");

            return DataHolder<CreditCard>.CreateSuccess(creditCard);
        }

        public async Task<DataHolder<CreditCard>> ChangePinAsync(ChangePinDto pinDto, Guid userId)
        {
            var creditCard = await GetCreditCardById(pinDto.Id, userId);

            if (creditCard == null)
                return DataHolder<CreditCard>.CreateFailure("Card don't exits");

            creditCard.Pin = pinDto.NewPin;

            var updateRes = await _creditCardRepository.UpdateAsync(creditCard);

            if (updateRes < 0)
                return DataHolder<CreditCard>.CreateFailure("Can't update credit card");

            return DataHolder<CreditCard>.CreateSuccess(creditCard);
        }

        public async Task<DataHolder<CreditCard>> ChangeCreditLimitAsync(ChangeCreditLimitDto limitDto, Guid userId)
        {
            if (!limitDto.Id.HasValue)
                return DataHolder<CreditCard>.CreateFailure("Invalid credit card Id");

            var creditCard = await GetCreditCardById(limitDto.Id.Value, userId);

            if (creditCard == null)
                return DataHolder<CreditCard>.CreateFailure("Credit card doesn't exist");

            creditCard.CreditLimit = limitDto.NewLimit;

            var updateResult = await _creditCardRepository.UpdateAsync(creditCard);

            if (updateResult < 0)
                return DataHolder<CreditCard>.CreateFailure("Can't update credit card");

            return DataHolder<CreditCard>.CreateSuccess(creditCard);
        }

        public async Task<DataHolder<CreditCard>> SendMoneyAsync(SendMoneyDto sendMoneyDto, Guid userId)
        {
            var fromCard = await _userRepository
                .Table
                .Where(user => user.Id == userId)
                .SelectMany(user => user.CreditCards)
                .FirstOrDefaultAsync(card => card.Id == sendMoneyDto.FromCardId);

            if (fromCard == null)
                return DataHolder<CreditCard>.CreateFailure("Incorrect user id or credit card number");

            var destCard = await _creditCardRepository
                .Table
                .FirstOrDefaultAsync(card => card.CardNumber == sendMoneyDto.ToCardNumber);

            if (destCard == null)
                return DataHolder<CreditCard>.CreateFailure("Invalid destination card number");

            var amount = await _convertCurrencyService.ConvertAsync(sendMoneyDto.Currency, (decimal)sendMoneyDto.Amount);

            if (!fromCard.SubtractMoney(amount))
                return DataHolder<CreditCard>.CreateFailure("Not enough money");
            if (!destCard.AddMoney(amount))
                return DataHolder<CreditCard>.CreateFailure("Exceeded the money limit");

            var updateResult = await _creditCardRepository.UpdateAsync(new List<CreditCard> { fromCard, destCard });

            if (updateResult <= 0)
                return DataHolder<CreditCard>.CreateFailure("Can't update balance");

            await _transactionMoneyLogRepository.InsertAsync(new []
            {
                new TransactionMoneyLog
                {
                    Amount = amount,
                    Currency = sendMoneyDto.Currency,
                    UserFromId = userId,
                    UserToId = destCard.UserId,
                    IsInput = false
                },
                new TransactionMoneyLog
                {
                    Amount = amount,
                    Currency = sendMoneyDto.Currency,
                    UserFromId = destCard.UserId,
                    UserToId = userId,
                    IsInput = true
                }
            });

            return DataHolder<CreditCard>.CreateSuccess(fromCard);
        }

        public async Task<DataHolder<Pager<TransactionMoneyLogDto>>> GetTransactionMoneyLogAsync(Guid cardId, int itemPerPage, int page)
        {
            var curUserInfo = await _userRepository
                .Table
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.CreditCards.Any(cc => cc.Id == cardId));

            if (curUserInfo == null)
                return DataHolder<Pager<TransactionMoneyLogDto>>.CreateFailure("Invalid card id");

            var logs = _transactionMoneyLogRepository
                .Table
                .Where(log => log.UserFromId == curUserInfo.Id);
            var pager = new Pager<TransactionMoneyLogDto>
            {
                Total = await logs.CountAsync(),
                Page = page,
                Data = await logs
                    .Join(_userRepository.Table,
                        log => log.UserToId,
                        u => u.Id,
                        (log, user) => new TransactionMoneyLogDto
                        {
                            Date = log.Date,
                            Currency = log.Currency,
                            Amount = log.Amount,
                            DestinationUser = new UserLogDto { Name = user.Name, LastName = user.LastName },
                            IsInput = log.IsInput
                        })
                    .Page(itemPerPage, page)
                    .ToListAsync()
            };


            return DataHolder<Pager<TransactionMoneyLogDto>>.CreateSuccess(pager);
        }

        private Task<CreditCard> GetCreditCardById(Guid id, Guid userId)
            => _creditCardRepository
            .Table
            .FirstOrDefaultAsync(card => card.UserId == userId && card.Id == id);
    }
}
