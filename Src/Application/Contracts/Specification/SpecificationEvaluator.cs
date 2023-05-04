using System.Linq.Expressions;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Specification;

public class SpecificationEvaluator<T> where T:BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputquery, ISpecification<T> specification)
    {
        var query = inputquery.AsQueryable();
        if (specification.Predicate != null)
        {
            query = query.Where(specification.Predicate);
        }

        if (specification.Includes.Any())
        {
            query = specification.Includes.Aggregate(query, (current, value) => current.Include(value));
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IspagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
       
        return query;
    }
}