using AutoMapper;
using OnlineBanking.Core.Models.DomainModels.User;
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
        }
    }
}
