using System;

namespace OnlineBanking.Core.Models.Dtos.User
{
    public class CreditCardDto
    {
        public string CardNumber { get; set; }

        public DateTime Expired { get; set; }

        public int Cvv { get; set; }
    }
}
