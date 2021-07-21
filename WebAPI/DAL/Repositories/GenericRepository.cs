using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public abstract class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext appContext;

        public GenericRepository(ApplicationContext context)
        {
            appContext = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await appContext.Set<T>().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await appContext.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await appContext.Set<T>().AddAsync(entity);
            await appContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            appContext.Set<T>().Update(entity);
            await appContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            appContext.Set<T>().Remove(entity);
            await appContext.SaveChangesAsync();
        }

        public bool Any(int id)
        {
            var entity = appContext.Set<T>().Find(id);
            if (entity == null)
            {
                return false;
            }
            else
            {
                appContext.Entry(entity).State = EntityState.Detached;
                return true;
            }
        }
    }
}
