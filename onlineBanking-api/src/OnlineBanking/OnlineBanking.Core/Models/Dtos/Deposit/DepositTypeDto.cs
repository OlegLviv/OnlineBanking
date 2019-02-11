using System;

namespace OnlineBanking.Core.Models.Dtos.Deposit
{
    public class DepositTypeDto
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public int Months { get; set; }

        public string Name { get; set; }

        public double Percentages { get; set; }
    }
}
