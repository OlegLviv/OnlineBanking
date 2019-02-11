using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBanking.DAL.Initializers.Abstract;

namespace OnlineBanking.DAL.Initializers.Executors
{
    public interface IInitializeExecutor
    {
        Task ExecuteAsync();
        ICollection<IInitializer> Initializers { get; }
    }
}
