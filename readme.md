## Potato Server para emburns

Este servidor está en desarrollo e incompleto
En algún momento voy a hacer un `bundle` para su fácil deploy sin romperse la cabeza (probablemente con Docker)

### ¿De qué se trata esto?

Esto es un proyecto que estoy haciendo para aprender y además poner a prueba cosas, a su vez realizar lo que todo el mundo hace: **una red social**

### ¿Por qué en ASP .NET?

Considero que es una buena forma de hacer un backend rápido, además de que es multiplataforma. 
Se puede usar Entity Framework para esconder esos SQL malditos y tener un código más limpio y actualizable.

### El anterior backend (Mokyu) que paso

QUE ES ESO, digo el anterior estaba en base a CodeIgniter y estaba recontra obsoleto, codigo inmantenible y PHP (una de las causas mayores)

### Requerimientos

- MariaDB (Recomendadísimo) >=10.3 o MySQL >= 8.0
- Visual Studio 2022 y/o dotnet6 (SDK, ASP SDK)
- Microsoft.EntityFrameworkCore.Design

### Deploy

No olvidar instalar Entity Framework 

`dotnet tool install --global dotnet-ef`

Y EF Desing (ver 6.0.13 obligatorio)

`dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.13`

#### Pasos

- Modificar `appsettings.json` el connection string de la base de datos MySQL (TIENE QUE SER MYSQL)

- Crear un Migration

`dotnet ef migrations add initial`

- Ejecutar update para crear la estructura en la base de datos

`dotnet ef database update`


Debug

`dotnet run --project .\emburns\emburns.csproj --configuration Debug`

Release

`dotnet run --project .\emburns\emburns.csproj --configuration Release`

En Debug se puede visualizar Swagger (api doc) //localhost:7010/swagger/index.html

Listo ✨

### Coso

Bueno falta mas desarrollo asi que eso bye

~~Odio Javascript~~