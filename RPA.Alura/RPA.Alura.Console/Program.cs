using Microsoft.Extensions.DependencyInjection;
using RPA.Alura.Application.Services;
using RPA.Alura.Domain.Repositories;
using RPA.Alura.Domain.Services;
using RPA.Alura.Infra.Repositories;
using RPA.Alura.Infra.Services;
using RPA.Alura.Infra.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        // Configurar o serviço de DI
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ISeleniumServices, SeleniumServices>()
            .AddSingleton<ICourseRepository, CourseRepository>(provider => new CourseRepository("Data Source=:memory:;Mode=Memory;Cache=Shared"))
            .AddSingleton<ICourseServices, CourseServices>()
            .BuildServiceProvider();

        var courseService = serviceProvider.GetService<ICourseServices>();

        courseService.Run();
    }
}
