namespace Api.Models
{
    public sealed record Product
    {
        public string Name { get; init; } = null!;

        public string Color { get; init; } = null!;

        public int QuantityPerUnit { get; init; }

        public decimal UnitPrice { get; init; }

        public int UnitsInStock { get; init; }

        public bool Discontinued { get; init; }
    }
}
