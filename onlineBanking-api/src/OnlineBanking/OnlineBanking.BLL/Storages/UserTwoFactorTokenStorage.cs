using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.Providers;

namespace OnlineBanking.BLL.Storages
{
    public class UserTwoFactorStaticTokenStorage : IUserTwoFactorTokenStorage
    {
        public ICollection<UserTwoFactorHolder> UserTwoFactorHolders { get; }

        public UserTwoFactorStaticTokenStorage()
        {
            UserTwoFactorHolders = new HashSet<UserTwoFactorHolder>();
        }

        public async Task<bool> AddAsync(UserTwoFactorHolder userTwoFactorHolder)
            => await Task.Run(() =>
            {
                UserTwoFactorHolders.Add(userTwoFactorHolder);

                return UserTwoFactorHolders.Any();
            });

        public async Task<bool> RemoveAsync(UserTwoFactorHolder userTwoFactorHolder)
            => await Task.Run(() => UserTwoFactorHolders.Remove(userTwoFactorHolder));

        public async Task<bool> RemoveRangeAsync(IEnumerable<UserTwoFactorHolder> userTwoFactorHolders)
            => await Task.Run(() => userTwoFactorHolders.All(holder => UserTwoFactorHolders.Remove(holder)));
    }
}
