using OnlineBanking.Core.Models.DomainModels.Abstract;
using System;

namespace OnlineBanking.Core.Models.DomainModels.Logs
{
    public class TransactionMoneyLog : Entity
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public bool IsInput { get; set; }
    }
}
