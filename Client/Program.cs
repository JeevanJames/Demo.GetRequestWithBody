using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using static System.Console;

namespace Client
{
    internal static class Program
    {
        private static async Task Main()
        {
            WriteLine("Waiting for API to start. Press ENTER when it has started...");
            ReadLine();

            var client = new ProductClient("https://localhost:5001", new HttpClient());

            var query = new ProductQuery
            {
                Color = "white",
                Pagination = new PaginationQuery
                {
                    Page = 2,
                    PageSize = 5
                }
            };

            // Using GET endpoint to query
            WriteLine("Results from GET endpoint:");
            ICollection<Product> getResults = await client.QueryAllAsync(query);
            foreach (Product product in getResults)
                WriteProduct(product);

            WriteLine();

            // Using POST endpoint to query
            WriteLine("Results from POST endpoint:");
            ICollection<Product> postResults = await client.QueryAsync(query);
            foreach (Product product in postResults)
                WriteProduct(product);

            WriteLine();
            Write("Press ENTER to quit...");
            ReadLine();
        }

        private static void WriteProduct(Product product)
        {
            WriteLine($"{product.Name} ({product.Color})");
        }
    }
}