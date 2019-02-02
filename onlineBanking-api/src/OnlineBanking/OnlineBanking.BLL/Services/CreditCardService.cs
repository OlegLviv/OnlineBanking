using System.Threading.Tasks;
using AutoMapper;
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

        public CreditCardService(IRepository<CreditCardOrder> creditCardOrderRepository, IMapper mapper)
        {
            _creditCardOrderRepository = creditCardOrderRepository;
            _mapper = mapper;
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
    }
}
