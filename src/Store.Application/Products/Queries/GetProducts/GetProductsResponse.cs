using System;
using System.Text.Json.Serialization;

namespace Store.Application.Products.Queries.GetProducts;

public sealed record GetProductsResponse
{
    public Guid Hash { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public DateTime? DeletedUTC { get; set; }
    public bool IsActive => !DeletedUTC.HasValue;
}