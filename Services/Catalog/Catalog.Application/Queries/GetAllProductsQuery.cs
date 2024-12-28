namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetAllProductsQuery : IRequest<IList<ProductResponse>>
{
    
}