using RPA.Alura.Domain.Entities;

namespace RPA.Alura.Infra.Services.Interfaces
{
    public interface ISeleniumServices
    {
        IEnumerable<Course> Get(string searchTerm);
        void Close();
    }
}
