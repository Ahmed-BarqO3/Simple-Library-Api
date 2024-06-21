using System.Linq.Expressions;
using LMS.Application.Interface;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;
public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
    where T : class, new()
{
    public Task AddAsync(T entity) =>

        Task.FromResult(context.Set<T>().AddAsync(entity));

    public Task Update(T entity) =>
    
        Task.FromResult(context.Set<T>().Update(entity));
    
    public Task Delete(T entity) =>
    
        Task.FromResult(context.Set<T>().Remove(entity));
       
    public Task<List<T>> GetByExecuteStoredProc(FormattableString commandName) =>
    
         context.Set<T>().FromSql(commandName).ToListAsync();

    public Task ExecuteStoredProcTask(FormattableString commandName)
    {
      return  Task.FromResult(context.Set<T>().FromSql(commandName));
    }

    public  Task<T?> FindAsync(Expression<Func<T, bool>> match, string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return  query.FirstOrDefaultAsync(match);
    }
    public Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, int? pageSize, int? pageNumber = null,
        string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>().Where(match);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);
        
        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
        }

        if (pageSize.HasValue)
        {
            query = query.Take(pageSize.Value);
        }

        return  query.ToListAsync();
    }

    public Task<List<T>> GetAllAsync(int? pageSize = null, int? pageNumber = null, string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
        }

        if (pageSize.HasValue)
        {
            query = query.Take(pageSize.Value);
        }

        return query.ToListAsync();
    }

    public ValueTask<T?> GetByIdAsync(int id)
    {
        return  context.Set<T>().FindAsync(id);
    }
}
