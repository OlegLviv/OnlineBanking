using System;
using OnlineBanking.Core.Models.Providers.Abstract;

namespace OnlineBanking.Core.Models.Providers
{
    public class UserTwoFactorHolder : IUserTwoFactorHolder<Guid>
    {
        public Guid Id { get; set; }
        public string TwoFactorCode { get; set; }

        public DateTime Expire { get; set; }

        public UserTwoFactorHolder(Guid id, string twoFactorCode)
        {
            Id = id;
            TwoFactorCode = twoFactorCode;
            Expire = DateTime.UtcNow.AddMinutes(1);
        }

        public UserTwoFactorHolder(Guid id, string twoFactorCode, DateTime expire) : this(id, twoFactorCode)
        {
            Expire = expire;
        }
    }
}
