using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Models.DomainModels;
using OnlineBanking.Core.Models.DomainModels.Logs;
using OnlineBanking.Core.Models.DomainModels.User;

namespace OnlineBanking.DAL
{
    public sealed class DataBaseContext : IdentityDbContext<User, IdentityRole, Guid>
    {
        public DataBaseContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TransactionMoneyLog> TransactionMoneyLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();
    }
}
