using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSProje.Models;

namespace BYSProje.Repositorys
{
    public interface IBYSRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T>GetByIDAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetByConditionAsync(int userNo, string password);
        Task<List<Student_Courses>> GetCoursesByStudentIdAsync(int studentId);
    }
}