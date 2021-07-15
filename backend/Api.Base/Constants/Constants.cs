namespace Api.Base.Constants
{
    public static class Constants
    {
        public static readonly int PAGE_INDEX_START = 1;
        public static readonly int PAGE_SIZE_MAX = 100;
        public static readonly int PAGE_SIZE_MIN = 5;
        /// <summary>
        /// Equivalent to HTTP status 200. OK indicates that the request succeeded and that the requested information is in the response. This is the most common status code to receive.
        /// </summary>
        public static readonly int OK = 200;
        /// <summary>
        /// Equivalent to HTTP status 201. Created indicates that the request resulted in a new resource created before the response was sent.
        /// </summary>
        public static readonly int CREATED= 201;
        /// <summary>
        /// Equivalent to HTTP status 204. NoContent indicates that the request has been successfully processed and that the response is intentionally blank.
        /// </summary>
        public static readonly int NOCONTENT = 204;
        public static readonly int SUCCESS = 299;
        public static readonly int EXPORT_LIMIT = 4000;
    }
}
