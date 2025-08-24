using WeatherApp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.DataAccess.Repositories
{
    internal class GenericRepository<TEntity>
       where TEntity : class
    {
        private readonly DataContext DbContext;

        public GenericRepository(DataContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
