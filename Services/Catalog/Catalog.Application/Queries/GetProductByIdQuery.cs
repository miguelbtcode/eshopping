namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetProductByIdQuery(string Id) : IRequest<ProductResponse>
{
    public string Id { get; } = Id;
}