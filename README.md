# TOTVS ATS (Projeto desafio não oficial)

## Versões

**Esse programa foi testado funcionando usando as seguintes versões:**

![](https://img.shields.io/badge/dotnet-v5.0.103-blue)
![](https://img.shields.io/badge/node-v14.16.1-blue)
![](https://img.shields.io/badge/npm-v7.11.2-blue)

## Como rodar

Primeiramente abrir o arquivo **HR.ATS.WebAPI/appsettings.Development.json**. E modificar o valor da url do banco para um banco mongo válido.

```json
"ConnectionStrings": {
  "mongoDb": "INSERIR URL AQUI"
}
```

```bash
# dotnet build ATS.sln
# dotnet run -p HR.ATS.WebAPI/HR.ATS.WebAPI.csproj 
```

**O projeto só estará pronto para acessar quando a seguinte mensagem aparecer no terminal:**

```
✔ Browser application bundle generation complete.
```
O frontend se encontra na pasta **HR.ATS.WebAPI/ClientApp/** em teoria durante o build da solução é rodado o **npm install**. Se der algum problema delete a **HR.ATS.WebAPI/ClientApp/node_modules** e rode o **npm install** na pasta **HR.ATS.WebAPI/ClientApp/**.

## Acessar o sistema

O sistema deve ser utilizado no **Chrome** e uma vez executado deve-se usar a seguinte url: **http://totvsats.localhost:5000**. Essa url tem o tenant atual da aplicação.

Todos os usuários possuem a senha **totvs@123**

Lista de usuários:
- candidato
- candidato2
- recrutador
