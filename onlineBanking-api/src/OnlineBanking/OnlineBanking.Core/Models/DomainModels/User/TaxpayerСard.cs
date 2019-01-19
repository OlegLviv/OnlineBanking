using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.User
{
    public class TaxpayerСard : Entity
    {
        public virtual  User User { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [MinLength(10)]
        public string Code { get; set; }

        public byte[] Photo { get; set; }
    }
}