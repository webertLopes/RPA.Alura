using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        var sqliteConnectionString = "Data Source=courses.db;";

        var serviceProvider = new ServiceCollection()
            .AddLogging(configure => configure.AddConsole())
            .AddSingleton<ISeleniumServices, SeleniumServices>()
            .AddSingleton<ICourseRepository, CourseRepository>(provider => new CourseRepository(sqliteConnectionString))
            .AddSingleton<ICourseServices, CourseServices>()
            .BuildServiceProvider();

        var courseService = serviceProvider.GetService<ICourseServices>();

        courseService.Run();
    }
}
