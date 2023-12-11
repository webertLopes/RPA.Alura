using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        public void Fechar()
        {
            throw new NotImplementedException();
        }

        public void NavegarPara(string url)
        {
            throw new NotImplementedException();
        }

        public string ObterConteudoDaPagina()
        {
            throw new NotImplementedException();
        }
    }
}
