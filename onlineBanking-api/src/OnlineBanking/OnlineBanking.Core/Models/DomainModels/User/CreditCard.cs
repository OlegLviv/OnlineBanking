﻿using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.User
{
    public class CreditCard : Entity
    {
        public virtual User User { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        public DateTime Expired { get; set; }

        [MinLength(100)]
        [MaxLength(999)]
        public int Cvv { get; set; }
    }
}
