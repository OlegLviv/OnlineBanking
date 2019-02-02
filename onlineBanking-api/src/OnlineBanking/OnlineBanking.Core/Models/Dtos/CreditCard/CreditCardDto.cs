using System;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class CreditCardDto
    {
        public Guid Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime Expired { get; set; }

        public int Cvv { get; set; }
    }
}
