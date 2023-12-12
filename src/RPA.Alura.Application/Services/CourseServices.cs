using Microsoft.Extensions.Logging;
using RPA.Alura.Domain.Repositories;
using RPA.Alura.Domain.Services;
using RPA.Alura.Infra.Services.Interfaces;

namespace RPA.Alura.Application.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISeleniumServices _seleniumServices;
        private readonly ILogger<ICourseServices> _logger;
        public CourseServices(ICourseRepository courseRepository, ISeleniumServices seleniumServices, ILogger<ICourseServices> logger)
        {
            _courseRepository = courseRepository;
            _seleniumServices = seleniumServices;
            _logger = logger;
        }

        public async Task Run()
        {
            await _courseRepository.Init();

            var courses = _seleniumServices.Get("RPA");

            foreach (var course in courses)            
                await _courseRepository.AddAsync(course);            

            _seleniumServices.Close();

            var coursesAll =  await _courseRepository.GetCoursesAsync();

            foreach (var item in coursesAll)
            {
                _logger.LogInformation($"Titulo: { item.Title }");
                _logger.LogInformation($"Carga horária: { item.Workload }");
                _logger.LogInformation($"Descrição : { item.Description }");
            }

            _logger.LogInformation("Fim da coleta.");
        }
    }
}
