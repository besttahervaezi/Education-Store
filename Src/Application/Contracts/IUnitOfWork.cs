using Domain.Entities.Base;

namespace Application.Contracts;
using  Microsoft.EntityFrameworkCore;
public interface IUnitOfWork
{
    DbContext context { get; }
    Task<int> Save(CancellationToken cancellationToken);
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
}