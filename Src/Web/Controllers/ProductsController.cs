using Application.Dtos.Products;
using Application.Features.Product.Queries.Get;
using Application.Features.Product.Queries.GetAll;
using Application.Features.ProductBrands.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] GetAllProductQuery request,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request,cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromRoute]int id,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetProductQuery(id), cancellationToken));
        }
        
    }
}
