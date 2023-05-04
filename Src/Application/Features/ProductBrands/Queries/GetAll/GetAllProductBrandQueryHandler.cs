using Application.Contracts;
using Application.Features.Product.Queries.GetAll;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductBrands.Queries.GetAll;

public class GetAllProductBrandQueryHandler:IRequestHandler<GetAllProductBrandQuery, IEnumerable<ProductBrand>>
{
    private readonly IUnitOfWork _UOW;

    public GetAllProductBrandQueryHandler(IUnitOfWork uow)
    {
        _UOW = uow;
    }

    public async Task<IEnumerable<ProductBrand>> Handle(GetAllProductBrandQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductBrandSpec();
        return await _UOW.Repository<ProductBrand>().ListAsyncSpec(spec, cancellationToken);
    }
}