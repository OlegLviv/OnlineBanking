using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Core.Models.Dtos.Deposit
{
    public class CreateDepositDto
    {
        [Required]
        public Guid DepositTypeId { get; set; }
    }
}
