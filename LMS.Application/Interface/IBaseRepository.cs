using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LMS.Application.Interface;
public interface IBaseRepository<T> where T : class, new()
{
    Task<List<T>> GetAllAsync(int? pageSize = null, int? pageNumber = null, string[]? includes = null);
    ValueTask<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate,string[]? includes = null);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int? pageSize = null, int? pageNumber = null,
        string[]? includes = null);
    Task<List<T>> GetByExecuteStoredProc(FormattableString commandName);
    Task ExecuteStoredProcTask(FormattableString commandName);
}
