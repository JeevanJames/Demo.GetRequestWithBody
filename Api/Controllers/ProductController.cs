using System;
using System.Collections.Generic;
using System.Linq;

using Api.Models;

using Bogus;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        ///     Lists of products.
        /// </summary>
        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> GetProducts()
        {
            return Products;
        }

        /// <summary>
        ///     Queries the products. You can access this endpoint either with GET or POST. In both
        ///     cases, you need to pass the query as a body payload.
        /// </summary>
        [HttpGet("_query", Name = "QueryProducts")]
        [HttpPost("_query", Name = "QueryProductsAlt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> QueryProducts([FromBody] ProductQuery query)
        {
            IEnumerable<Product> result = Products;

            if (!string.IsNullOrWhiteSpace(query.Name))
                result = result.Where(p => p.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(query.Color))
                result = result.Where(p => p.Color.Equals(query.Color, StringComparison.OrdinalIgnoreCase));

            PaginationQuery pagination = query.Pagination ?? PaginationQuery.Default;
            if (pagination.PageSize < 1)
                pagination = pagination with { PageSize = 10 };
            result = result
                .Skip(pagination.Page * pagination.PageSize)
                .Take(pagination.PageSize);

            return result.ToList();
        }

        private static readonly List<Product> Products = new Faker<Product>()
            .UseSeed(1)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Color, f => f.Commerce.Color())
            .RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.QuantityPerUnit, f => f.Random.Number(1, 10))
            .RuleFor(p => p.UnitsInStock, f => f.Random.Number(0, 1000))
            .RuleFor(p => p.Discontinued, f => f.Random.Bool(0.2f))
            .Generate(500);
    }
}
