namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetProductsByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>
{
    public string BrandName { get; } = BrandName;
}