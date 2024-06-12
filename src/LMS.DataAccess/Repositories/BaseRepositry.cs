using System.Linq.Expressions;
using LMS.Application.Interface;
using LMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    private readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity) =>

        await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) =>

      _context.Set<T>().Update(entity);

    public void Delete(T entity) =>
    
        _context.Set<T>().Remove(entity);
       
    public IQueryable<T?> ExecuteStoredProc(string commandName) =>
    
        _context.Set<T>().FromSqlRaw(commandName);
    
    public async Task<T?> FindAsync(Expression<Func<T, bool>> match) =>
    
         await _context.Set<T>().FindAsync(match);
    
    public async Task<IEnumerable<T?>> GetAllAsync() =>
 
         await _context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
    
         await _context.Set<T>().FindAsync(id);

  
       

}
