namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetProductByIdQuery(string id) : IRequest<ProductResponse>
{
    public string Id { get; set; } = id;
}