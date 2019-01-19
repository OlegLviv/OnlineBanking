using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Models.DomainModels;

namespace OnlineBanking.DAL
{
    public sealed class DataBaseContext : IdentityDbContext<User, IdentityRole, Guid>
    {
        public DataBaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DataBaseContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();
    }
}
