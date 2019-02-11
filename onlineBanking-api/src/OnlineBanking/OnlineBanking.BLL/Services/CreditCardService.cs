using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.CreditCard;
using OnlineBanking.DAL;

namespace OnlineBanking.BLL.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly IRepository<CreditCardOrder> _creditCardOrderRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<CreditCard> _creditCardRepository;

        public CreditCardService(IRepository<CreditCardOrder> creditCardOrderRepository,
            IMapper mapper,
            IRepository<CreditCard> creditCardRepository)
        {
            _creditCardOrderRepository = creditCardOrderRepository;
            _mapper = mapper;
            _creditCardRepository = creditCardRepository;
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
            if(!limitDto.Id.HasValue)
                return DataHolder<CreditCard>.CreateFailure("Invalid credit card Id");

            var creditCard = await GetCreditCardById(limitDto.Id.Value, userId);

            if(creditCard == null)
                return DataHolder<CreditCard>.CreateFailure("Credit card doesn't exist");

            creditCard.CreditLimit = limitDto.NewLimit;

            var updateResult = await _creditCardRepository.UpdateAsync(creditCard);

            if(updateResult < 0)
                return DataHolder<CreditCard>.CreateFailure("Can't update credit card");

            return DataHolder<CreditCard>.CreateSuccess(creditCard);
        }

        private Task<CreditCard> GetCreditCardById(Guid id, Guid userId)
            => _creditCardRepository
            .Table
            .FirstOrDefaultAsync(card => card.UserId == userId && card.Id == id);
    }
}
