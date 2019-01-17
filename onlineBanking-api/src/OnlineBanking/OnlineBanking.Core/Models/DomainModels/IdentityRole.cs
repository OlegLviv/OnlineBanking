using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineBanking.Core.Models.DomainModels
{
    public class IdentityRole : IdentityRole<Guid>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid();
        }

        public IdentityRole(string name) : this()
        {
            Name = name;
        }
    }
}
