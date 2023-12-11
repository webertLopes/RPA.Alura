using Dapper;
using Microsoft.Data.Sqlite;
using RPA.Alura.Domain.Entities;
using RPA.Alura.Domain.Repositories;
using System.Data;

namespace RPA.Alura.Infra.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbConnection _dbConnection;
        public CourseRepository(string connectionString)
        {
            _dbConnection = new SqliteConnection(connectionString);
        }
        public async Task AddAsync(Course course)
        {
            var sql = @"
            INSERT INTO Courses (Title, Instructor, Workload, Description)
            VALUES (@Title, @Instructor, @Workload, @Description);";
            await _dbConnection.ExecuteAsync(sql, course);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            var sql = "SELECT Id, Title, Instructor, Workload, Description  FROM Courses;";
            return await _dbConnection.QueryAsync<Course>(sql);
        }
    }
}
