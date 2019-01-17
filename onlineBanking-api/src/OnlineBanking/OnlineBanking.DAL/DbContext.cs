using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Models.DomainModels;

namespace OnlineBanking.DAL
{
    public sealed class DbContext : IdentityDbContext<User, IdentityRole, Guid>
    {
        public DbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();
    }
}
