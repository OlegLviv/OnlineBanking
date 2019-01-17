using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineBanking.Core.Models.DomainModels
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public override Guid Id { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
