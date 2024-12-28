namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetProductByNameQuery(string Name) : IRequest<IList<ProductResponse>>
{
    public string Name { get; } = Name;
}