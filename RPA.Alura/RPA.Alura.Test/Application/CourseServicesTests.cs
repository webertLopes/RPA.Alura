using Microsoft.Extensions.Logging;
using Moq;
using RPA.Alura.Application.Services;
using RPA.Alura.Domain.Entities;
using RPA.Alura.Domain.Repositories;
using RPA.Alura.Infra.Services.Interfaces;

[TestFixture]
public class CourseServicesTests
{
    private Mock<ICourseRepository> _mockCourseRepository;
    private Mock<ISeleniumServices> _mockSeleniumServices;
    private Mock<ILogger<CourseServices>> _mockLogger;
    private CourseServices _courseServices;

    [SetUp]
    public void Setup()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _mockSeleniumServices = new Mock<ISeleniumServices>();
        _mockLogger = new Mock<ILogger<CourseServices>>();

        _courseServices = new CourseServices(_mockCourseRepository.Object, _mockSeleniumServices.Object, _mockLogger.Object);

        _mockCourseRepository.Setup(repo => repo.Init()).Returns(Task.CompletedTask);
        _mockCourseRepository.Setup(repo => repo.AddAsync(It.IsAny<Course>())).Returns(Task.CompletedTask);
        _mockCourseRepository.Setup(repo => repo.GetCoursesAsync()).ReturnsAsync(new List<Course>());

        var courses = new List<Course>
        {
            new Course { Title = "Java Course 1", Instructor = "Instructor 1", Workload = "10h", Description = "Description 1" },
            new Course { Title = "Java Course 2", Instructor = "Instructor 2", Workload = "20h", Description = "Description 2" }
        };

        _mockSeleniumServices.Setup(service => service.Get("Java")).Returns(courses);
        _mockSeleniumServices.Setup(service => service.Close());
    }

    [Test]
    public async Task Run_ShouldInitializeRepositoryAndProcessCourses()
    {
        // Act
        await _courseServices.Run();

        // Assert
        _mockCourseRepository.Verify(repo => repo.Init(), Times.Once);
        _mockCourseRepository.Verify(repo => repo.AddAsync(It.IsAny<Course>()), Times.Exactly(2));
        _mockCourseRepository.Verify(repo => repo.GetCoursesAsync(), Times.Once);
        _mockSeleniumServices.Verify(service => service.Get("Java"), Times.Once);
        _mockSeleniumServices.Verify(service => service.Close(), Times.Once);
    }
}
