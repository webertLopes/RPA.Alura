namespace RPA.Alura.Infra.Services.Interfaces
{
    public interface ISeleniumServices
    {
        void NavegarPara(string url);
        string ObterConteudoDaPagina();
        void Fechar();
    }
}
