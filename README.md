# RPA Cursos Alura

O "RPA Cursos Alura" é uma aplicação de RPA (Automação Robótica de Processos) desenvolvida em .NET 6, destinada a obter informações sobre cursos disponíveis no site da [Alura](https://www.alura.com.br/). Utiliza tecnologias como Dapper, Selenium WebDriver e outras ferramentas de automação web para extrair dados de maneira eficiente e estruturada.

## Tecnologias Utilizadas

- .NET 6
- Dapper
- Selenium WebDriver
- SQLite (Banco de Dados)

## Pré-requisitos

Para executar este projeto, você precisará ter o seguinte instalado:
- .NET 6 SDK
- Um navegador compatível com o Selenium WebDriver (como o Chrome ou Firefox)
- O driver correspondente ao navegador escolhido (como chromedriver ou geckodriver)

## Instalação e Configuração

1. Clone o repositório do GitHub:

git clone https://github.com/webertLopes/RPA.Alura

2. Navegue até a pasta do projeto:

cd RPA.Alura

3. Restaure as dependências do projeto:

dotnet restore


## Como Usar

Para iniciar a aplicação, navegue até a pasta do projeto e execute o comando:

dotnet run

Isso iniciará o processo de automação, onde a aplicação acessará o site da Alura e começará a coleta de dados dos cursos disponíveis.

Para visualizar os dados coletados no SQLite, instale a extensão SQLite Viewer no Chrome e abra o arquivo de banco de dados localizado em: src\RPA.Alura.Console\bin\Debug\net6.0\courses 


## Executando os Testes

Para executar os testes unitários, use o comando:


dotnet test

Isso executará todos os testes unitários definidos no projeto, garantindo que as principais funcionalidades estejam funcionando conforme esperado.

## Contribuição

Contribuições para o projeto são sempre bem-vindas. Se você tem uma sugestão para melhorar este RPA, sinta-se à vontade para criar uma issue ou pull request no GitHub.

## Autores

- Webert Lopes - Desenvolvedor do projeto

## Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

## Contato

- Webert Lopes - webert.lopes.tecnologia@gmail.com
