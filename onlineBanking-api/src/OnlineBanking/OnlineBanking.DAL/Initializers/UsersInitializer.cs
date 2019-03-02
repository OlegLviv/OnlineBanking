using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Roles;
using OnlineBanking.DAL.Initializers.Abstract;

namespace OnlineBanking.DAL.Initializers
{
    public class UsersInitializer : IInitializer
    {
        public async Task InitAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (await userManager.Users.AnyAsync())
                    return;

                var user = new User
                {
                    Name = "User",
                    LastName = "User",
                    Email = "olehspidey@gmail.com",
                    PhoneNumber = "+380680538860",
                    UserName = "user"
                };

                var card = new CreditCard
                {
                    CardNumber = "5323435234234624",
                    Expired = DateTime.UtcNow.AddYears(2),
                    Cvv = 123,
                    Type = CreditCardType.MasterCard,
                    CreditLimit = 30000,
                    UserId = user.Id
                };

                var card2 = new CreditCard
                {
                    CardNumber = "3423986987658906",
                    Expired = DateTime.UtcNow.AddYears(3),
                    Cvv = 187,
                    Type = CreditCardType.Visa,
                    CreditLimit = 10000,
                    UserId = user.Id
                };

                user.CreditCards = new List<CreditCard> { card, card2 };

                var user2 = new User
                {
                    Name = "User2",
                    LastName = "User2",
                    Email = "olehspidey2@gmail.com",
                    PhoneNumber = "+380680538861",
                    UserName = "user2"
                };

                var card3 = new CreditCard
                {
                    CardNumber = "5323435234234625",
                    Expired = DateTime.UtcNow.AddYears(3),
                    Cvv = 123,
                    Type = CreditCardType.MasterCard,
                    CreditLimit = 30000,
                    Balance = 100,
                    UserId = user2.Id
                };
                var card4 = new CreditCard
                {
                    CardNumber = "3423986987658907",
                    Expired = DateTime.UtcNow.AddYears(4),
                    Cvv = 187,
                    Type = CreditCardType.MasterCard,
                    CreditLimit = 10000,
                    Balance = 345,
                    UserId = user2.Id
                };

                user2.CreditCards = new List<CreditCard> { card3, card4 };
                await userManager.CreateAsync(user, "30Test30");
                await userManager.CreateAsync(user2, "30Test30");
                await userManager.AddToRoleAsync(user, UserRoles.User);
                await userManager.AddToRoleAsync(user2, UserRoles.User);
            }
        }

        public int Priority => 2;
    }
}
