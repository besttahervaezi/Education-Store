using Application.Contracts;
using Domain.Entities.Base;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class UnitofWork:IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitofWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbContext context => _context;
    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        return new GenericRepository<T>(_context);
    }
}