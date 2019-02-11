using System;
using System.Threading.Tasks;

namespace OnlineBanking.DAL.Initializers.Abstract
{
    public interface IInitializer
    {
        Task InitAsync(IServiceProvider serviceProvider);

        int Priority { get; }
    }
}
