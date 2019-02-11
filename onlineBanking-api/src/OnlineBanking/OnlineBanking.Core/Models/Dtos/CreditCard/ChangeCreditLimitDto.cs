using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class ChangeCreditLimitDto
    {
        public Guid? Id { get; set; }

        [Range(0, 100000)]
        public int NewLimit { get; set; }
    }
}
