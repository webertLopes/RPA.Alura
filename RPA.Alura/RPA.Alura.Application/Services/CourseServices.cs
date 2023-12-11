using RPA.Alura.Domain.Repositories;
using RPA.Alura.Domain.Services;
using RPA.Alura.Infra.Services.Interfaces;

namespace RPA.Alura.Application.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISeleniumServices _seleniumServices;
        public CourseServices(ICourseRepository courseRepository, ISeleniumServices seleniumServices)
        {
            _courseRepository = courseRepository;
            _seleniumServices = seleniumServices;
        }

        public Task Run()
        {
            throw new NotImplementedException();
        }
    }
}
