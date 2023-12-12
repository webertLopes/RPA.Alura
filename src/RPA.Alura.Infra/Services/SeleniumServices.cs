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
            // Obs: Pode-se Uar ao Inves de By.CssSelector o By.XPath para navegar entre os elementos.
            
            
            // Fluxo de Extração
            
            
            // 1 - Acessar URL alura https://www.alura.com.br/
            _webDriver.Navigate().GoToUrl("https://www.alura.com.br/");

            // 2 - Encontrar a barra de pesquisas e buscas da Alura cursos
            var searchField = _webDriver.FindElement(By.CssSelector(".header__nav--busca-input"));

            // 3 - Passar o termo escolhido na barra de pesquisas e buscas da Alura cursos
            searchField.SendKeys(searchTerm);

            // 4 - Encontrar o botão ou a lupa para pesquisar 
            var searchButton = _webDriver.FindElement(By.CssSelector(".header__nav--busca-submit"));

            // 5 - Clicar no botão ou lupa de pesquisa
            searchButton.Click();

            var courses = new List<Course>();

            // 6 - Encontrar os elementos de cursos para extração
            var courseElements = _webDriver.FindElements(By.CssSelector(".busca-resultado"));


            foreach (var element in courseElements)
            {
                // 7 - Extrair titulo
                var title = element.FindElement(By.CssSelector(".busca-resultado-nome")).Text;

                // 8 - Extrair Descrição
                var description = element.FindElement(By.CssSelector(".busca-resultado-descricao")).Text;

                // 8 - Extrair Url que levará a pagina de instrutores e horas aula
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
            // 9 - Acessar a url de instrutores e horas aula
            _webDriver.Navigate().GoToUrl(course.Url);

            // Esta etapa necessitou uma validação especial pois em alguns casos os
            // identificadores de classe eram diferentes consegui identificar dois casos distintos 

            // 10 - Extração de instrutores e Carga Horária
            // 1º Caso quando temos apenas um instrutor ele faz este processo abaixo
            var instructors = _webDriver.FindElements(By.CssSelector(".instructor-title--name"));
            var instructor = instructors.FirstOrDefault()?.Text;

            var workloads = _webDriver.FindElements(By.CssSelector(".courseInfo-card-wrapper-infos"));
            var workload = workloads.FirstOrDefault()?.Text;

            // 2º Caso ele encontra 2 instrutores e faz o processo abaixo
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
