using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Extensions;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.User.Register;

namespace OnlineBanking.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private const int MaxFileSize = 3000_000;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<DataHolder<User>> GetFromIdentityAsync(string identityName)
        {
            if (identityName == null)
                throw new ArgumentNullException(nameof(identityName));

            var userData = await _userManager
                .Users
                .FirstOrDefaultAsync(user => user.UserName == identityName ||
                                             user.Email == identityName ||
                                             user.PhoneNumber == identityName);

            if(userData == null)
                return DataHolder<User>.CreateUnauthorized();

            return DataHolder<User>.CreateSuccess(userData);
        }

        public async Task<DataHolder<string>> GetUserId(string identityName)
        {
            if (identityName == null)
                throw new ArgumentNullException(nameof(identityName));

            var userId = await _userManager
                .Users
                .Where(user => user.UserName == identityName || user.Email == identityName)
                .Select(user => user.Id)
                .FirstOrDefaultAsync();

            if(userId == Guid.Empty)
                return DataHolder<string>.CreateUnauthorized();

            return DataHolder<string>.CreateSuccess(userId.ToString());
        }

        public async Task<DataHolder<User>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
                throw new ArgumentNullException(nameof(registerUserDto));

            if (registerUserDto.TaxpayerСard.Photo.Length > MaxFileSize ||
               registerUserDto.Passport.FirsPage.Length > MaxFileSize ||
               registerUserDto.Passport.SecondPage.Length > MaxFileSize ||
               registerUserDto.Passport.ResidencePage.Length > MaxFileSize)
                return DataHolder<User>.CreateFailure($"File size can't be > {MaxFileSize} bytes");

            var user = _mapper.Map<RegisterUserDto, User>(registerUserDto);
            var passportDto = registerUserDto.Passport;

            SetTaxpayedToUser(user, registerUserDto.TaxpayerСard.Code, await WriteStreamToBufferAsync(registerUserDto.TaxpayerСard.Photo.OpenReadStream()));
            SetPassportToUser(user,
                await WriteStreamToBufferAsync(passportDto.FirsPage.OpenReadStream()),
                await WriteStreamToBufferAsync(passportDto.SecondPage.OpenReadStream()),
                await WriteStreamToBufferAsync(passportDto.ResidencePage.OpenReadStream()));

            var createdStatus = await _userManager.CreateAsync(user, registerUserDto.Password);

            if(!createdStatus.Succeeded)
                return DataHolder<User>.CreateFailure(createdStatus.CreateErrorsString());

            return DataHolder<User>.CreateSuccess(user);
        }

        private async Task<byte[]> WriteStreamToBufferAsync(Stream stream)
        {
            using (stream)
            {
                var buffer = new byte[stream.Length];

                await stream.WriteAsync(buffer, 0, (int)stream.Length);

                return buffer;
            }
        }

        private static void SetTaxpayedToUser(User user, string code, byte[] photo)
            => user.TaxpayerСard = new TaxpayerСard
            {
                User = user,
                UserId = user.Id,
                Code = code,
                Photo = photo
            };

        private static void SetPassportToUser(User user, byte[] firstPage, byte[] secondPage, byte[] residencePage)
            => user.Passport = new Passport
            {
                User = user,
                UserId = user.Id,
                FirsPage = firstPage,
                SecondPage = secondPage,
                ResidencePage = residencePage
            };
    }
}
