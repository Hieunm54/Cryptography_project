namespace Api.Base.Pagination.Base
{
    public interface IPageable
    {
        int PageNumber { set; get; }
        int PageSize { set; get; }
        Sort Sort { set; get; }
        int LoadMode { get; set; }
    }
}
