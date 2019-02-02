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
                &&(string.IsNullOrWhiteSpace(dto.City)
                || string.IsNullOrWhiteSpace(dto.FlatNumber)
                || string.IsNullOrWhiteSpace(dto.HouseNumber)))
                return DataHolder<CreditCardOrder>.CreateFailure("City or flat number order house number is empty");

            creditCardOrder.Status = CreditCardOrderStatus.New;
            creditCardOrder.User = user;

            var insertResult = await _creditCardOrderRepository.InsertAsync(creditCardOrder);

            if(insertResult < 0 )
                return DataHolder<CreditCardOrder>.CreateFailure("Cant create order");

            return DataHolder<CreditCardOrder>.CreateSuccess(creditCardOrder);
        }

        public async Task<DataHolder<CreditCard>> GetById(Guid id, Guid userId)
        {
            var creditCard = await _creditCardRepository
                .Table
                .FirstOrDefaultAsync(card => card.UserId == userId && id == card.Id);

            if(creditCard == null)
                return DataHolder<CreditCard>.CreateFailure("Card doesn't exist");

            return DataHolder<CreditCard>.CreateSuccess(creditCard);
        }
    }
}
