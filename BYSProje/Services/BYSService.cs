using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSProje.Repositorys;
namespace BYSProje.Services
{
    public class BYSService<T> : IBYSService<T> where T: class
    {
         private readonly BYSRepository<T> _repository;

        public  BYSService(BYSRepository<T> repository)
        {
             _repository = repository;
        }
    
    
        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
           
            if (id !=null)
            {
                var service = id;
                await _repository.DeleteAsync(service);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _repository.GetByIDAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
             await _repository.UpdateAsync(entity);
        }
    }

   
}