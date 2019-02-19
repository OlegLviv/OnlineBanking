namespace OnlineBanking.Core.Models.Dtos.CreditCard
{
    public class SendMoneyDto
    {
        public string Currency { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public string ToCardNumber { get; set; }

        public string FromCardNumber { get; set; }
    }
}
