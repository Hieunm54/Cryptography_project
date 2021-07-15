using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Base.Pagination.Base
{
    public class Page<T> : IPage<T> where T : class
    {
        public int PageIndex { get; set; }
        public long TotalItem { get; set; }
        public IEnumerable<T> Content { get; set; }
        public Sort Sort { get; set; }
    }
}
