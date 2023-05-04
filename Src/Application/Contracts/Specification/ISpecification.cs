using System.Linq.Expressions;
using Domain.Entities.Base;

namespace Application.Contracts.Specification;

public interface ISpecification<T> where  T:BaseEntity
{
    Expression<Func<T,bool>> Predicate { get; }
    List<Expression<Func<T,object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDesc { get; }

    public int Take { get; }
    public int Skip { get;  }
    public bool IspagingEnabled { get; set; } 
}