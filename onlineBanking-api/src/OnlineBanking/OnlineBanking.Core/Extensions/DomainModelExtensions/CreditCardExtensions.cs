using System;
using OnlineBanking.Core.Models.DomainModels.CreditCard;

namespace OnlineBanking.Core.Extensions.DomainModelExtensions
{
    public static class CreditCardExtensions
    {
        public static bool AddMoney(this CreditCard creditCard, decimal money)
        {
            if (creditCard.Balance - creditCard.Credit + money > creditCard.CreditLimit)
                return false;

            var f = creditCard.Credit - money;

            if (f >= 0)
            {
                creditCard.Credit -= money;

                return true;
            }

            creditCard.Credit -= money - Math.Abs(f);
            creditCard.Balance += Math.Abs(f);

            return true;
        }

        public static bool SubtractMoney(this CreditCard creditCard, decimal money)
        {
            if (creditCard.Credit == creditCard.CreditLimit)
                return false;

            if (money > (creditCard.Balance + creditCard.CreditLimit - creditCard.Credit))
                return false;

            var f = creditCard.Balance - money;

            if (f >= 0)
            {
                creditCard.Balance -= money;

                return true;
            }

            creditCard.Balance -= creditCard.Balance;
            creditCard.Credit += Math.Abs(f);

            return true;
        }
    }
}
