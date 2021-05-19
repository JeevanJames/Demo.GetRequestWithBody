namespace Api.Models
{
    public sealed record ProductQuery
    {
        public string? Name { get; init; }

        public string? Color { get; init; }

        public PaginationQuery? Pagination { get; init; }
    }
}