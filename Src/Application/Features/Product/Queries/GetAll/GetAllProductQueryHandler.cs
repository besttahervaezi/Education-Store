using Application.Contracts;
using Application.Dtos.Products;
using Application.wrappers;
using AutoMapper;
using MediatR;
using Domain.Entities;
namespace Application.Features.Product.Queries.GetAll;

public class GetAllProductQueryHandler:IRequestHandler<GetAllProductQuery,PaginationResponse<ProductDto>>
{
    private readonly IUnitOfWork _UOW;
    private readonly IMapper _mapper;
    public GetAllProductQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _UOW = uow;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpec(request);
        var count = await _UOW.Repository<Domain.Entities.Product>().CountAsyncSpec(spec, cancellationToken);
        var result= await _UOW.Repository<Domain.Entities.Product>().ListAsyncSpec(spec,cancellationToken);
        var model= _mapper.Map<IEnumerable<ProductDto>>(result);
        return new PaginationResponse<ProductDto>(request.PageIndex, request.PageSize, count, model);
    }
   
}