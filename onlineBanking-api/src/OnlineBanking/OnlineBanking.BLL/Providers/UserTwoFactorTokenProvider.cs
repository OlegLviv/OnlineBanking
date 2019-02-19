using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OnlineBanking.BLL.Storages;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Providers;

namespace OnlineBanking.BLL.Providers
{
    public class UserTwoFactorTokenProvider : IUserTwoFactorTokenProvider<User>
    {
        private readonly IUserTwoFactorTokenStorage _storage;
        private readonly IConfiguration _configuration;
        private const int MinCodeValue = 100_000;
        private const int MaxCodeValue = 999_999;

        public UserTwoFactorTokenProvider(IUserTwoFactorTokenStorage storage, IConfiguration configuration)
        {
            _storage = storage;
            _configuration = configuration;
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            var expiredHolders = _storage
                .UserTwoFactorHolders
                .Where(x => x.Expire - DateTime.UtcNow < TimeSpan.Zero)
                .ToList();

            if (expiredHolders.Any())
                await _storage.RemoveRangeAsync(expiredHolders);

            var currentHolder = _storage
                .UserTwoFactorHolders
                .FirstOrDefault(x => x.Id == user.Id);

            if (currentHolder != null)
                return null;

            var code = new Random().Next(MinCodeValue, MaxCodeValue).ToString();

            await _storage.AddAsync(new UserTwoFactorHolder(user.Id, code, DateTime.UtcNow.AddMinutes(int.Parse(_configuration["TwoFactorLifeTime"]))));

            return code;
        }

        public async Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            var twoFactorHolder = _storage
                .UserTwoFactorHolders
                .FirstOrDefault(holder => holder.TwoFactorCode == token);

            if (twoFactorHolder == null || twoFactorHolder.Expire - DateTime.UtcNow < TimeSpan.Zero)
                return false;

            return await _storage.RemoveAsync(twoFactorHolder);
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
            => Task.FromResult(true);
    }
}
