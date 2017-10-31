using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure
{
    public sealed class QueryResult<TResult>
    {
        public TResult[] Results { get; set; }
        public int TotalFound { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int TotalPages
        {
            get
            {
                return PageSize != 0
                    ? (int)Math.Ceiling((decimal)TotalFound / PageSize)
                    : 0;
            }
        }
    }
}
