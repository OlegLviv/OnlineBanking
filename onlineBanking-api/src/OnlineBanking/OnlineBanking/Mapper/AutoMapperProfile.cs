using AutoMapper;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos.CreditCard;
using OnlineBanking.Core.Models.Dtos.Deposit;
using OnlineBanking.Core.Models.Dtos.User.Register;

namespace OnlineBanking.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(user => user.TaxpayerСard, expression => expression.Ignore())
                .ForMember(user => user.Passport, expression => expression.Ignore());

            CreateMap<CreateCreditCardOrderDto, CreditCardOrder>()
                .ForMember(user => user.Id, expression => expression.Ignore())
                .ForMember(user => user.Status, expression => expression.Ignore());

            CreateMap<DepositType, DepositTypeDto>()
                .ForMember(dto => dto.IsTaken, expression => expression.Ignore());
        }
    }
}
