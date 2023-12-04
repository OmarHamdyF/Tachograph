using Core.Application.Interfaces;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private ApplicationDbContext context;
    protected DbSet<T> objectSet;

    public BaseRepository(ApplicationDbContext context)
    {

        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        this.context = context;
        this.objectSet = context.Set<T>();
    }

    public virtual void Add(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        objectSet.Add(entity);

    }

    public virtual void Remove(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        objectSet.Remove(entity);
    }

    public DbContext Context
    {
        get
        {
            return context;
        }
    }
    public virtual void AddRange(IEnumerable<T> entities)
    {

        if (entities == null)
        {
            throw new ArgumentNullException("entities is null");
        }
        objectSet.AddRange(entities);
    }

    public async virtual Task AddRangeAsync(IEnumerable<T> entities)
    {

        if (entities == null)
        {
            throw new ArgumentNullException("entities is null");
        }
        await objectSet.AddRangeAsync(entities);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {

        if (entities == null)
        {
            throw new ArgumentNullException("entities is null");
        }
        objectSet.RemoveRange(entities);
    }

    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entities is null");
        }
        context.Entry(entity).State = EntityState.Modified;
        objectSet.Update(entity);

    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException("entities is null");
        }
        objectSet.UpdateRange(entities);
    }
    public async Task<List<T>> GetAll()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        context.ChangeTracker.Clear();
        return entity;
    }

    public void Load(T entity, Expression<Func<T, object>> propertyExpression)
    {
        context.Entry(entity).Reference(propertyExpression).Load();
    }
}

