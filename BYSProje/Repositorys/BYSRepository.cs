using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSProje.DBContext.Entity;
using Microsoft.EntityFrameworkCore;
namespace BYSProje.Repositorys
{
    public class BYSRepository<T> : IBYSRepository<T> where T : class
    {

         private readonly BYSContext _context;
         private readonly DbSet<T> _dbSet;
         
          public BYSRepository(BYSContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            
        }
        public async Task AddAsync(T entity)
        {   
          
             await _dbSet.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIDAsync(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
           return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
             await _context.SaveChangesAsync();

        }
    }
}