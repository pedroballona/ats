# TOTVS ATS (Projeto desafio não oficial)

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

## Acessar o sistema

O sistema deve ser utilizado no **Chrome** e uma vez executado deve-se usar a seguinte url: **http://totvsats.localhost:5000**. Essa url tem o tenant atual da aplicação.

Todos os usuários possuem a senha **totvs@123**

Lista de usuários:
- candidato
- candidato2
- recrutador
