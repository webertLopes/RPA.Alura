using RPA.Alura.Domain.Entities;

namespace RPA.Alura.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task AddAsync(Course course);
    }
}
