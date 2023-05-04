using Application.Contracts;
using MediatR;

namespace Application.Features.ProductType.Queries.GetAll;

public class GetAllProductTypeQueryHandler:IRequestHandler<GetAllProductTypeQuery,IEnumerable<Domain.Entities.ProductType>>
{
    private readonly IUnitOfWork _UOW;

    public GetAllProductTypeQueryHandler(IUnitOfWork uow)
    {
        _UOW = uow;
    }

    public async Task<IEnumerable<Domain.Entities.ProductType>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
    {
        return await _UOW.Repository<Domain.Entities.ProductType>().GetAllAsync(cancellationToken);
    }
}