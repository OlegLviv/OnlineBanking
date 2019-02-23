using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class SendMoneyDto
    {
        [Required]
        public string Currency { get; set; }

        [Range(0D, 100_000)]
        public double Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public string ToCardNumber { get; set; }

        [Required]
        public Guid FromCardId { get; set; }
    }
}
