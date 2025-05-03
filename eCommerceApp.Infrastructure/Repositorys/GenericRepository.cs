using eCommerceApp.Application.Exceptions;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositorys;
public class GenericRepository<TEntity>(ApplicationDbContext context) : IGeneric<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        return await _context.SaveChangesAsync();
    }
    public async Task<int> DeleteAsync(Guid id)
    {
        if(await _context.Set<TEntity>().FindAsync(id) is not { } entity)
            return 0;

        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        if (await _context.Set<TEntity>().FindAsync(id) is not { } entity)
            throw new ItemNotFoundException($"{typeof(TEntity).Name} is not found");

        return entity;
    }
    public async Task<int> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return await _context.SaveChangesAsync();
    }
}
