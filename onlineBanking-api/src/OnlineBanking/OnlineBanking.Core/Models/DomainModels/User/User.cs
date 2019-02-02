using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Core.Models.DomainModels.Abstract;
using OnlineBanking.Core.Models.DomainModels.CreditCard;

namespace OnlineBanking.Core.Models.DomainModels.User
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public override Guid Id { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public virtual Address ResidenceAddress { get; set; }

        public virtual TaxpayerСard TaxpayerСard { get; set; }

        public virtual Passport Passport { get; set; }

        public virtual List<CreditCard.CreditCard> CreditCards { get; set; }

        public virtual List<CreditCardOrder> CreditCardOrders { get; set; }
    }
}
