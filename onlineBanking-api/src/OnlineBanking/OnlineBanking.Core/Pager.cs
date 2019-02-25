using System.Collections.Generic;

namespace OnlineBanking.Core
{
    public class Pager<TData>
    {
        public IEnumerable<TData> Data { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
