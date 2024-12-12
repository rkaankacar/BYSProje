using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSProje.Models;
using BYSProje.Repositorys;
namespace BYSProje.Services
{
    public class BYSService<T> : IBYSService<T> where T: class
    {
         private readonly IBYSRepository<T> _repository;

        public  BYSService(IBYSRepository<T> repository)
        {
             _repository = repository;
        }
    
    
        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
           
            if (id > 0)
            {
                await _repository.DeleteAsync(id);
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

           public async Task<T> GetByConditionAsync(int userNo, string password)
        {
            return await _repository.GetByConditionAsync(userNo, password);
        }







          public async Task<List<CourseViewModel>> GetCoursesByStudentIdAsync(int studentId)
           {
    
                var studentCourses = await _repository.GetCoursesByStudentIdAsync(studentId);

                if (studentCourses == null || !studentCourses.Any())
                {
                   return null;
                }

    
                 return studentCourses.Select(sc => new CourseViewModel
                  {
                    CourseName = sc.Course.CourseName,
                     Credits = sc.Course.Credits,
                        CourseID = sc.Course.CourseID
                   }).ToList();
            }














    }
        
   
}