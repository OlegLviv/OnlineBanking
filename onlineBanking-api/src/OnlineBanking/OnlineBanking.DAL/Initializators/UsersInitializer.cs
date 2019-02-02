using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Roles;

namespace OnlineBanking.DAL.Initializators
{
    public static class UsersInitializer
    {
        public static async Task InitAsync(UserManager<User> userManager)
        {
            if(await userManager.Users.AnyAsync())
                return;

            var user = new User
            {
                Name = "User",
                LastName = "User",
                Email = "olehspidey@gmail.com",
                PhoneNumber = "+380680538860",
                UserName = "user"
            };

            await userManager.CreateAsync(user, "30Test30");
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }
    }
}
