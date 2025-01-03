namespace Catalog.Application.Queries;

using Core.Specs;
using MediatR;
using Responses;

public sealed record GetAllProductsQuery(CatalogSpecParams CatalogSpecParams) : IRequest<Pagination<ProductResponse>>;