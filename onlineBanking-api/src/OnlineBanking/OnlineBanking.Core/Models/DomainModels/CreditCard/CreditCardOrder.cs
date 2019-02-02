using System;
using OnlineBanking.Core.Models.DomainModels.Abstract;

namespace OnlineBanking.Core.Models.DomainModels.CreditCard
{
    public class CreditCardOrder : Entity
    {
        public CreditCardOrderDeliveryType DeliveryType { get; set; }

        public CreditCardOrderStatus Status { get; set; }

        public string City { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        public virtual User.User User { get; set; }

        public Guid UserId { get; set; }
    }
}
