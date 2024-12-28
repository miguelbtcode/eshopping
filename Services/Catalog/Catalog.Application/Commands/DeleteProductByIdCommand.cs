namespace Catalog.Application.Commands;

using MediatR;

public sealed record DeleteProductByIdCommand 
(
    string Id
): IRequest<bool>;