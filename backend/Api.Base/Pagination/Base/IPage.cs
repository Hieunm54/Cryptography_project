using System.Collections.Generic;

namespace Api.Base.Pagination.Base
{
    public interface IPage<T> where T : class
    {
        public int PageIndex { get; set; }
        public long TotalItem { get; set; }
        public IEnumerable<T> Content { get; set; }
        public Sort Sort { get; set; } 
    }
}
