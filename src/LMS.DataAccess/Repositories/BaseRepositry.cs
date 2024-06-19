using System.Linq.Expressions;
using LMS.Application.Interface;
using LMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;
public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
    where T : class, new()
{
    public async Task AddAsync(T entity) =>

        await context.Set<T>().AddAsync(entity);

    public void Update(T entity) =>

      context.Set<T>().Update(entity);

    public void Delete(T entity) =>
    
        context.Set<T>().Remove(entity);
       
    public async Task<IEnumerable<T?>> ExecuteStoredProc(string commandName) =>
    
       await context.Set<T>().FromSqlRaw(commandName).ToListAsync();

    public async Task<T?> FindAsync(Expression<Func<T, bool>> match, string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes != null)
            foreach (var incluse in includes)
                query = query.Include(incluse);

        return await query.FirstOrDefaultAsync(match);
    }
    public async Task<IEnumerable<T?>> FindAllAsync(Expression<Func<T, bool>> match,int? pageSize,int? pageNumber, string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>().Where(match);

        if (includes is not null)
            foreach (var incluse in includes)
                query = query.Include(incluse);
        
        if (pageNumber.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
        }

        if (pageSize.HasValue)
        {
            query = query.Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }


    public async Task<IEnumerable<T?>> GetAllAsync(int? pageSize,int? pageNumber, string[]? includes = null)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes is not null)
            foreach (var incluse in includes)
                query = query.Include(incluse);
        
        if (pageNumber.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value);
        }

        if (pageSize.HasValue)
        {
            query = query.Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }
}
