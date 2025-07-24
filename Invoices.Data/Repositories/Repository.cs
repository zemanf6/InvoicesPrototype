using Invoices.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /*
     * Případně lze nad rámec zadání implementovat metody GetAll, GetById, SaveChanges, Add
     * asynchronně (async, await)
     * EF poskytuje metody ToListAsync(), FindAsync(), SaveChangesAsync(), AddAsync() 
    */ 
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
