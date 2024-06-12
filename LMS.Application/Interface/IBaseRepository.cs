using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace LMS.Application.Interface;
public interface IBaseRepository<T> where T : class, new()
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T?> ExecuteStoredProc(string commandName);
}
