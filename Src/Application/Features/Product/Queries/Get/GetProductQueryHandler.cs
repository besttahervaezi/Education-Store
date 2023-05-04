using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.Product.Queries.GetAll;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Product.Queries.Get;

public class GetProductQueryHandler:IRequestHandler<GetProductQuery,ProductDto>
{
    private readonly IUnitOfWork _UOW;
    private readonly IMapper _mapper;
    public GetProductQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _UOW = uow;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetProductSpec(request.Id);
       var result= await _UOW.Repository<Domain.Entities.Product>().GetEntityWithSpec(spec, cancellationToken);
       if (result==null)
       {
           throw new NotFoundException();
       }
       return _mapper.Map<ProductDto>(result);
    }
}