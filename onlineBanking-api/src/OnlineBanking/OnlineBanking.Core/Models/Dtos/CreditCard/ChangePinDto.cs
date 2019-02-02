using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class ChangePinDto
    {
        [Range(0, 9999)]
        public int NewPin { get; set; }

        public Guid Id { get; set; }
    }
}
