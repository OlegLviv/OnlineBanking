using System.Linq;

namespace OnlineBanking.Core.Extensions.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int itemPerPage, int page)
            => source
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage);
    }
}
