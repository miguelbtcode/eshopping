namespace Catalog.API.Controllers;

using System.Net;
using Application.Commands;
using Application.Queries;
using Application.Responses;
using Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class CatalogController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet] 
    [Route("[action]/{productName}", Name = "GetProductByProductName")] 
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet] 
    [Route("GetAllProducts")] 
    [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var query = new GetAllProductsQuery(catalogSpecParams);
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet] 
    [Route("GetAllBrands")] 
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet] 
    [Route("GetAllTypes")] 
    [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet] 
    [Route("[action]/{brandName}", Name = "GetProductsByBrandName")] 
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetProductsByBrandName(string brandName)
    {
        var query = new GetProductsByBrandQuery(brandName);
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand productCommand)
    {
        var result = await mediator.Send(productCommand);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteProduct(string id)
    {
        var command = new DeleteProductByIdCommand(id);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}