using Dapper;
using Microsoft.Data.Sqlite;
using RPA.Alura.Domain.Entities;
using RPA.Alura.Infra.Repositories;
using System.Data;

[TestFixture]
public class CourseRepositoryTests
{
    private CourseRepository _repository;
    private IDbConnection _dbConnection;

    [SetUp]
    public async Task Setup()
    {
        var connectionString = "Data Source=courses.db;";
        _dbConnection = new SqliteConnection(connectionString);
        _dbConnection.Open();

        _repository = new CourseRepository(connectionString);
        await _repository.Init();
    }

    [TearDown]
    public void TearDown()
    {
        _dbConnection.Close();
    }

    [Test]
    public async Task AddAsync_ShouldAddCourse()
    {
        var course = new Course
        {
            Title = "Test Course",
            Instructor = "Test Instructor",
            Workload = "5h",
            Description = "Test Description"
        };

        await _repository.AddAsync(course);

        var courses = await _repository.GetCoursesAsync();
        Assert.That(courses, Has.Some.Matches<Course>(c => c.Title == "Test Course"));
    }

    [Test]
    public async Task GetCoursesAsync_ShouldReturnAllCourses()
    {
        var courses = await _repository.GetCoursesAsync();
        Assert.That(courses, Is.Not.Null);
    }

    [Test]
    public async Task Init_ShouldCreateCoursesTable()
    {
        // O método Init é chamado no Setup. Podemos verificar se a tabela foi criada corretamente.
        var count = await _dbConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Courses';");
        Assert.That(count, Is.EqualTo(1));
    }
}
