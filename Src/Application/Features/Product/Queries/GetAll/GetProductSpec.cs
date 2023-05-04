using System.Linq.Expressions;
using Application.Contracts.Specification;
using Application.wrappers;

namespace Application.Features.Product.Queries.GetAll;

public class GetProductSpec:BaseSpecification<Domain.Entities.Product>
{
    public GetProductSpec(GetAllProductQuery specParams) :base(GetAll.Expression.ExpressionSpec(specParams) 

                                                               )
    {
        //include
        AddIncludes(x=>x.productbrand);
        AddIncludes(x=>x.producttype);
        //sort
        if (specParams.Typesort==RequestParametersBasic.TypeSort.Desc)
        {
            switch (specParams.Sort)
            {
                case 1:
                    AddOrderByDesc(x=>x.Title);
                    break;
                case 2:
                    AddOrderByDesc(x=>x.producttype.Title);
                    break;
                default:
                    AddOrderByDesc(x=>x.Title);
                    break;
            }
        }
        else
        {
            switch (specParams.Sort)
            {
                case 1:
                    AddOrderBy(x => x.Title);
                    break;
                case 2:
                    AddOrderBy(x => x.producttype.Title);
                    break;
                default:
                    AddOrderBy(x => x.Title);
                    break;
            }

        }
        ApplyPageing(specParams.PageSize*(specParams.PageIndex-1),specParams.PageSize,true);
    }

    

    public GetProductSpec(int id):base(x=>x.Id==id)
    {
        AddIncludes(x=>x.productbrand);
        AddIncludes(x => x.producttype);
    }
}

public static class Expression
{
    public static Expression<Func<Domain.Entities.Product, bool>> ExpressionSpec(GetAllProductQuery specParams)
    {
        return x => (String.IsNullOrEmpty(specParams.Search) || x.Title.ToLower().Contains(specParams.Search)) && (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
                    (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId);
    }
}

public class ProductCountSpec :BaseSpecification<Domain.Entities.Product>
{
    public ProductCountSpec(GetAllProductQuery specParams) :base(Expression.ExpressionSpec(specParams))
    {
        IspagingEnabled = false;
    }
}