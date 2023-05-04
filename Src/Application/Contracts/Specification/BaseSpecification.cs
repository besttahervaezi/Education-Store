using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Application.Contracts.Specification;

public class BaseSpecification<T> :ISpecification<T> where T:BaseEntity
{
    public Expression<Func<T, bool>> Predicate { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDesc { get; private set; }
    public int Take  { get; set; }
    public int Skip { get; set; }
    public bool IspagingEnabled { get; set; } = true;

    public BaseSpecification()
    {
        
    }

    public BaseSpecification(Expression<Func<T,bool>> predicate)
    {
        Predicate = predicate;
    }

    protected void AddIncludes(Expression<Func<T,object>> include)
    {
        Includes.Add(include);
    }
    protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }
    protected void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
    {
        OrderBy = OrderByDescExpression;
    }

    protected void ApplyPageing(int skip, int take, bool ispagingEnabled = true)
    {
        Skip = skip;
        Take = take;
        IspagingEnabled = ispagingEnabled;
    }

}