using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.CreditCard
{
    public class CreditCard : Entity
    {
        public virtual User.User User { get; set; }

        public Guid UserId { get; set; }

        [Required, CreditCard]
        public string CardNumber { get; set; }

        public DateTime Expired { get; set; }

        [MinLength(100), MaxLength(999)]
        public int Cvv { get; set; }

        [Range((int)CreditCardType.Visa, (int)CreditCardType.MasterCard)]
        public CreditCardType Type { get; set; }

        [Range(0, 9999)]
        public int Pin { get; set; }

        [Range(0, 100000)]
        public int CreditLimit { get; set; }

        [Range(0, 100000)]
        public decimal Balance { get; set; }

        public decimal Credit { get; set; }

        public decimal Deposit { get; set; }
    }
}
