namespace Catalog.Application.Queries;

using MediatR;
using Responses;

public sealed record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
{
    
}