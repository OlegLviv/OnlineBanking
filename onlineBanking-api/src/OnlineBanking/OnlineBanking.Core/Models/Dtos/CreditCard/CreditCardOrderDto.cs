using System;
using OnlineBanking.Core.Models.DomainModels.CreditCard;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class CreditCardOrderDto
    {
        public Guid Id { get; set; }

        public CreditCardOrderDeliveryType DeliveryType { get; set; }

        public CreditCardOrderStatus Status { get; set; }

        public string City { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }
    }
}
