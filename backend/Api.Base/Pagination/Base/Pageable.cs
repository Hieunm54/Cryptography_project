namespace Api.Base.Pagination.Base
{
    public class Pageable : IPageable
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Sort Sort { get; set; }
        public int LoadMode { get; set; }
    }
}
