using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Roles;

namespace OnlineBanking.DAL.Initializators
{
    public static class UsersInitializer
    {
        public static async Task InitAsync(UserManager<User> userManager)
        {
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
                        CardNumber = "53234352342346",
                        Expired = DateTime.UtcNow.AddYears(2),
                        Cvv = 123,
                        Type = CreditCardType.MasterCard
                    }
                }
            };

            await userManager.CreateAsync(user, "30Test30");
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }
    }
}
