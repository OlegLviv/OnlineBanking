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
                    UserName = "user",
                    CreditCards = new List<CreditCard>
                    {
                        new CreditCard
                        {
                            CardNumber = "5323435234234624",
                            Expired = DateTime.UtcNow.AddYears(2),
                            Cvv = 123,
                            Type = CreditCardType.MasterCard,
                            CreditLimit = 30000
                        },
                        new CreditCard
                        {
                            CardNumber = "3423986987658906",
                            Expired = DateTime.UtcNow.AddYears(3),
                            Cvv = 187,
                            Type = CreditCardType.Visa,
                            CreditLimit = 10000
                        }
                    }
                };

                await userManager.CreateAsync(user, "30Test30");
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }

        public int Priority => 2;
    }
}
