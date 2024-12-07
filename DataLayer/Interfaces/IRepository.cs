using System.Linq.Expressions;

namespace DataAccessLayer.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> AsQueryable();
    IQueryable<T>? Where(Expression<Func<T, bool>> predicate);
    Task<T>? FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    void Delete(T entity);
    T? Find(params object?[]? keyValues);
    Task<T>? FindAsync(params object?[]? keyValues);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    Task<dynamic> GetEntityMaxId(Expression<Func<T, dynamic>> predicate);
    Task Commit();
    Task<List<T>> InsertRangeAsync(List<T> entities);
}
