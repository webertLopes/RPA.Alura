using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RPA.Alura.Domain.Entities;
using RPA.Alura.Infra.Services.Interfaces;

namespace RPA.Alura.Infra.Services
{
    public class SeleniumServices : ISeleniumServices
    {
        private readonly IWebDriver _webDriver;

        public SeleniumServices()
        {
            _webDriver = new ChromeDriver();
        }

        public void Close()
        {
            _webDriver.Close();
            _webDriver.Dispose();
        }

        public IEnumerable<Course> Get(string searchTerm)
        {
            _webDriver.Navigate().GoToUrl("https://www.alura.com.br/");

            var searchField = _webDriver.FindElement(By.CssSelector(".header__nav--busca-input"));
            searchField.SendKeys(searchTerm);

            var searchButton = _webDriver.FindElement(By.CssSelector(".header__nav--busca-submit"));
            searchButton.Click();

            var courses = new List<Course>();

            var courseElements = _webDriver.FindElements(By.CssSelector(".busca-resultado"));

            foreach (var element in courseElements)
            {
                var title = element.FindElement(By.CssSelector(".busca-resultado-nome")).Text;
                var description = element.FindElement(By.CssSelector(".busca-resultado-descricao")).Text;
                var url = element.FindElement(By.CssSelector(".busca-resultado-link"))?.GetAttribute("href");

                if (url is not null && (title.StartsWith("Curso") || title.StartsWith("Formação")))
                {
                    var course = new Course
                    {
                        Title = title,
                        Description = description,
                        Url = url
                    };

                    courses.Add(course);
                }
            }

            return CaptureDataByCourse(courses);
        }

        private IEnumerable<Course> CaptureDataByCourse(IEnumerable<Course> courses)
        {
            foreach (var course in courses.ToList())            
                ProcessCourse(course);            

            return courses;
        }

        private void ProcessCourse(Course course)
        {
            _webDriver.Navigate().GoToUrl(course.Url);

            var instructors = _webDriver.FindElements(By.CssSelector(".instructor-title--name"));
            var instructor = instructors.FirstOrDefault()?.Text;

            var workloads = _webDriver.FindElements(By.CssSelector(".courseInfo-card-wrapper-infos"));
            var workload = workloads.FirstOrDefault()?.Text;

            if (string.IsNullOrEmpty(instructor) && string.IsNullOrEmpty(workload))
            {
                var instructorElements = _webDriver.FindElements(By.CssSelector(".formacao-instrutor-nome"));
                var instructorNames = instructorElements.Select(instructor => instructor.Text).ToList();

                var instance = string.Join(", ", instructorNames);

                var firstInstructor = instance.Split(',').Select(name => name.Trim()).Where(name => !string.IsNullOrEmpty(name)).FirstOrDefault();

                if (!string.IsNullOrEmpty(firstInstructor))
                    course.Instructor = firstInstructor;

                var schedule = _webDriver.FindElements(By.CssSelector(".formacao__info-destaque"));
                var load = schedule.FirstOrDefault()?.Text;

                if (!string.IsNullOrEmpty(load))
                    course.Workload = load;
            }

            if (!string.IsNullOrEmpty(instructor))
                course.Instructor = instructor;

            if (!string.IsNullOrEmpty(workload))
                course.Workload = workload;
        }
    }
}
