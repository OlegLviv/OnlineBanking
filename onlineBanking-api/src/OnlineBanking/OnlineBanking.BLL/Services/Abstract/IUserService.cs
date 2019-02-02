using System;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.User.Register;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface IUserService
    {
        Task<DataHolder<User>> GetFromIdentityAsync(string identityName);

        Task<DataHolder<string>> GetUserId(string identityName);

        Task<DataHolder<User>> RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
