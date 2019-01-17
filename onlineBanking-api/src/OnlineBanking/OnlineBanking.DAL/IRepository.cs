using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Core.Models.DomainModels;

namespace OnlineBanking.DAL
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Table { get; }

        Task<TEntity> GetByIdAsync(object id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(ICollection<TEntity> entities);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(ICollection<TEntity> entities);
    }
}
