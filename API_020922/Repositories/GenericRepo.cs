using API_020922.Contexts;
using Microsoft.EntityFrameworkCore;

namespace API_020922.Repositories
{
    public interface IGenericRepo<T>
    {
        public Task<T> AddAsync(T obj);
        public Task SaveChangesAsync();
        public Task<IEnumerable<T>> GetAllAsync();
        public T Update(T obj);
    }

    public class GenericRepo<T> : IGenericRepo<T> where T: class
    {
        IAppDBContext db;
        DbSet<T> table;

        public GenericRepo(IAppDBContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public async Task<T> AddAsync(T obj)
        {
            var res = await table.AddAsync(obj);
            return res.Entity;
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public T Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            return obj;
        }
    }
}
