namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetProductByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>
{
    public string BrandName { get; set; } = BrandName;
}