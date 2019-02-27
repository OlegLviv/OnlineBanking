using System;
using OnlineBanking.Core.Models.Dtos.User;

namespace OnlineBanking.Core.Models.Dtos.Logs
{
    public class TransactionMoneyLogDto
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public UserLogDto DestinationUser { get; set; }

        public bool IsInput { get; set; }
    }
}
