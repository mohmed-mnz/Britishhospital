using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BritshHosbitalContext _context;
    private DbSet<T> entity;
    public Repository(BritshHosbitalContext context)
    {
        _context = context;
        entity = context.Set<T>();
    }
    public IQueryable<T> AsQueryable()
    {
        return entity.AsQueryable();
    }
    public IQueryable<T>? Where(Expression<Func<T, bool>> predicate)
    {
        return entity.Where(predicate);
    }

    public async Task<T>? FirstOrDefaultAsync(Expression<Func<T, bool>> predicates)
    {
        return await entity.FirstOrDefaultAsync(predicates);
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await entity.SingleOrDefaultAsync(predicate);
    }

    public async Task InsertAsync(T entity)
    {
        await this.entity.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        this.entity.Remove(entity);
    }

    public T? Find(params object?[]? keyValues)
    {
        return entity.Find(keyValues);
    }

    public async Task<T>? FindAsync(params object?[]? keyValues)
    {
        return await entity.FindAsync(keyValues);
    }

    private void SetDefaultValues()
    {


    }

    private bool IsEntityDerivedFromBaseClass(object entity, Type baseDomain)
    {
        Type entityType = entity.GetType();
        bool isEntityDerived = baseDomain.IsAssignableFrom(entityType);
        return isEntityDerived;
    }

    private bool EntityHasProperty(object entity, string property)
    {
        Type entityType = entity.GetType();
        return entityType.GetProperty(property) != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await entity.ToListAsync();
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
    {
        return await entity.Where(predicate).ToListAsync();
    }

    public async Task<dynamic> GetEntityMaxId(Expression<Func<T, dynamic>> predicate)
    {
        return await entity.MaxAsync(predicate);
    }

    public async Task Commit()
    {
        SetDefaultValues();
        await _context.SaveChangesAsync();
    }


}