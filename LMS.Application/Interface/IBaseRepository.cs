using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace LMS.Application.Interface;
public interface IBaseRepository<T> where T : class, new()
{
    Task<IEnumerable<T?>> GetAllAsync(int? pageSize = null,int? pageNumber = null,string[]? includes = null);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate,string[]? includes = null);
    Task<IEnumerable<T?>>FindAllAsync(Expression<Func<T, bool>> predicate,int? pageSize = null,int? pageNumber = null,string[]? includes = null);
    Task<IEnumerable<T?>> ExecuteStoredProc(string commandName);
}
