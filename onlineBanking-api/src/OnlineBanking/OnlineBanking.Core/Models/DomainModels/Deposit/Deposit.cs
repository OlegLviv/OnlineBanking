using System;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.Deposit
{
    public class Deposit : Entity
    {
        public DateTime Expire { get; set; }

        public virtual User.User User { get; set; }

        public Guid UserId { get; set; }

        public virtual DepositType DepositType { get; set; }

        public virtual Guid DepositTypeId { get; set; }
    }
}
