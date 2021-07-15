using Api.Base.Enums;

namespace Api.Base.Pagination.Base
{
    public class Sort
    {
        public string Field { get; set; } = "";
        public Direction Direction { get; set; } = Direction.Asc;
    }
}
