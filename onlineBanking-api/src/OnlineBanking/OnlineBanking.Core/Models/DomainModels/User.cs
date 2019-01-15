using Microsoft.AspNetCore.Identity;

namespace OnlineBanking.Core.Models.DomainModels
{
    public class User : IdentityUser, IEntity<string>
    {
    }
}
