# InvestiCalc

Uma aplicação de exemplo ilustrando o uso de Angular com uma WebAPI ASP.NET Core / .NET 7

## Requisitos

* NodeJS : Baixe e instale [por aqui](https://nodejs.org/en). Para maior comodidade, certifique-se que o caminho do runtime do Node e seus utilitários (especificamente npm) estejam na variável de ambiente PATH do sistema. As instruções para preparação da aplicação Angular dependem disto.
* Angular/CLI : Após instalado o Node, execute `npm install -g @angular/cli` em um terminal.
* [.NET 7 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)

## Preparando o ambiente

* Clone o repositório: `git clone git@github.com:fernandofig/investicalc.git`
* Abra um terminal na pasta criada após clonado o repositório
* Instale os módulos usados no front:
```
   cd InvestmentCalculator.WebFront
   npm install
```

## Rodando a aplicação em ambiente de Desenvolvimento

* Abra mais uma janela de terminal na raiz da pasta criada ao ser clonado o repositório
* Nesta janela de terminal, execute: `dotnet run -lp https --project ./InvestmentCalculator.WebAPI/InvestmentCalculator.WebAPI.csproj`
* Na janela de terminal anterior, que deverá estar ainda na pasta da aplicação de frontend (`InvestmentCalculator.WebFront`), execute: `ng serve`
* A aplicação estará acessível via browser no endereço: http://localhost:4200/
* O Swagger da WebAPI pode ser acessada no endereço https://localhost:7037/swagger/index.html
