# Assumptions/Prerequisites

Please ensure that the software/modules listed below have been installed in your system.

1. SQL Server
2. SQL Server Management Studio
3. npm/ Node.js
4. Visual Studio 2022 or higher

<br/>

# Configurations

To begin configuring, clone the repo as follows:

```git
git clone https://github.com/aditya1962/Repos
```

Then, open appsettings.json in WebAPIMC folder. Replace the connection string with that used with your SQL Server instance. For example, in the following change the server name, user id and password.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConnectionString": "Server=.\\MSSQL2;Database=MCCatalog;User Id=sa; Password=12341234;TrustServerCertificate=True;"
  }
}
```

Open the following (in Scripts folder) in the order given in SQL Server Management Studio and execute each script:

1. 01_DB creation script.sql
2. 02_InsertData.sql
3. 03_sp_InsertInvoiceData.sql
4. 04_sp_InsertInvoiceProductData.sql
5. 05_sp_GetProducts.sql

<br/>

# Usage

Open a command line inside the WebAPIMC folder and run the following to start APIs:

```cmd
dotnet run
```

Open a command line inside the WebUI folder and run the following to start the UI:

```cmd
npm run build
```