using Application.Features.ProductBrands.Queries.GetAll;
using Application.Features.ProductType.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers
{
    public class ProductTypeControllerController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> Get(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductTypeQuery(), cancellationToken));
        }
    }
}
