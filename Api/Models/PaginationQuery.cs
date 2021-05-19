namespace Api.Models
{
    public sealed record PaginationQuery
    {
        public int Page { get; init; }

        public int PageSize { get; init; }

        public static readonly PaginationQuery Default = new()
        {
            Page = 0,
            PageSize = 10,
        };
    }
}