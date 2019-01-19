using System;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.User
{
    public class Address : BaseAddress
    {
        public virtual User User { get; set; }

        public Guid UserId { get; set; }
    }
}
