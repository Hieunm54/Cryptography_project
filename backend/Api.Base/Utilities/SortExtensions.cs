using Api.Base.Enums;

namespace Api.Base.Utilities
{
    public static class SortExtensions
    {
        public static bool IsDescending(this Direction direction)
        {
            return direction.Equals(Direction.Desc);
        }

        public static bool IsAscending(this Direction direction)
        {
            return direction.Equals(Direction.Asc);
        }
    }
}
