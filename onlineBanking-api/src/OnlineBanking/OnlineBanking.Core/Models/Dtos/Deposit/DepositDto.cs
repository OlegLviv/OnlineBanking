using System;

namespace OnlineBanking.Core.Models.Dtos.Deposit
{
    public class DepositDto
    {
        public Guid Id { get; set; }

        public DateTime Expire { get; set; }

        public DepositTypeDto DepositType { get; set; }
    }
}
