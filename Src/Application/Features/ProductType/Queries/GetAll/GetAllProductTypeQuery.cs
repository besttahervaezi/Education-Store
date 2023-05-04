using MediatR;

namespace Application.Features.ProductType.Queries.GetAll;

public class GetAllProductTypeQuery:IRequest<IEnumerable<Domain.Entities.ProductType>>
{
    
}