using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.DAL.Initializers.Abstract;

namespace OnlineBanking.DAL.Initializers
{
    public class DepositTypesInitializer : IInitializer
    {
        public async Task InitAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<DepositType>>();

                if (await repository.Table.AnyAsync())
                    return;

                var depositF = new DepositType
                {
                    Currency = Currency.Uah,
                    Name = "6 month deposit",
                    Months = 6,
                    Percentages = 21
                };
                var depositS = new DepositType
                {
                    Currency = Currency.Uah,
                    Name = "3 month deposit",
                    Months = 3,
                    Percentages = 15
                };
                var depositT = new DepositType
                {
                    Currency = Currency.Uah,
                    Name = "Super",
                    Months = 4,
                    Percentages = 23
                };

                await Task.WhenAll(
                    repository.InsertAsync(depositF),
                    repository.InsertAsync(depositS),
                    repository.InsertAsync(depositT)
                );
            }
        }

        public int Priority => 3;
    }
}
