using OnlineBanking.Core.Models.DomainModels.Abstract;
using System;

namespace OnlineBanking.Core.Models.DomainModels.Logs
{
    public class TransactionMoneyLog : Entity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public User.User UserFrom { get; set; }

        public Guid UserFromId { get; set; }

        public User.User UserTo { get; set; }

        public Guid UserToId { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }
    }
}
