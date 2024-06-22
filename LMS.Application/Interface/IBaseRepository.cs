using System.Linq.Expressions;

namespace LMS.Application.Interface;
public interface IBaseRepository<T> where T : class, new()
{
    Task<List<T>> GetAllAsync(CancellationToken ct,int? pageSize = null, int? pageNumber = null, string[]? includes = null);
    ValueTask<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate,string[]? includes = null,CancellationToken? ct = null);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int? pageSize = null, int? pageNumber = null,
        string[]? includes = null,CancellationToken? ct = null);
    Task<List<T>> GetByExecuteStoredProc(FormattableString commandName);
    Task ExecuteStoredProcTask(FormattableString commandName);
}
