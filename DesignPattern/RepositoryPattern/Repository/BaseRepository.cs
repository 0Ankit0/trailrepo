using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern.Repository
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Task BeginTransaction()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            return _context.Database.CommitTransactionAsync();
        }

        public Task Rollback()
        {
            return _context.Database.RollbackTransactionAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
              _dbSet.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
