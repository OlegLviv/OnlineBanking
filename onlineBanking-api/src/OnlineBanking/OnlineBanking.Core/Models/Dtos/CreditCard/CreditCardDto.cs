using System;
using OnlineBanking.Core.Models.DomainModels.CreditCard;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class CreditCardDto
    {
        public Guid Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime Expired { get; set; }

        public CreditCardType Type { get; set; }
    }
}
