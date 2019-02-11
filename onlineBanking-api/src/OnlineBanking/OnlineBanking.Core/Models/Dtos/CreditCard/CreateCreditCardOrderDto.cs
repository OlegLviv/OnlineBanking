using OnlineBanking.Core.Models.DomainModels.CreditCard;

namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class CreateCreditCardOrderDto
    {
        public CreditCardOrderDeliveryType DeliveryType { get; set; }

        public string City { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }
    }
}
