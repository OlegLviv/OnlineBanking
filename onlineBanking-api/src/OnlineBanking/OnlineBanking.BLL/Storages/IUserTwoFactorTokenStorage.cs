using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.Providers;

namespace OnlineBanking.BLL.Storages
{
    public interface IUserTwoFactorTokenStorage
    {
        ICollection<UserTwoFactorHolder> UserTwoFactorHolders { get; }

        Task<bool> AddAsync(UserTwoFactorHolder userTwoFactorHolder);

        Task<bool> RemoveAsync(UserTwoFactorHolder userTwoFactorHolder);

        Task<bool> RemoveRangeAsync(IEnumerable<UserTwoFactorHolder> userTwoFactorHolders);
    }
}
