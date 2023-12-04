using Core.Application.Interfaces;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    #region IUnitOfWork Members
    public int Complete()
    {
        if (_context == null)
            throw new InvalidOperationException("Context has not been initialized.");
        return _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
        if (_context == null)
            throw new InvalidOperationException("Context has not been initialized.");
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    #endregion
}

