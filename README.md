# Azure Storage Demo With Azure Table and CosmosDB

- Using DotNetCore 2.2 MVC for the Demo

#### DotNet commands

- dotnet new web --name Contacts2TableCosmosMVC -f netcoreapp2.2
- DotNet Packages for the Project
  - Azure Storage
    - dotnet add package WindowsAzure.Storage -v 9.3.3 (or latest)
  - Azure CosmosDB API
  - Using .Net 3.0
    - dotnet add package Microsoft.Azure.Cosmos -v 3.6.0 for using CosmosClient as the class
  - Using .Net 2.0
    - dotnet add package Microsoft.Azure.DocumentDB.Core -v 2.9.2 DocumentClient as the class
      - namespace to be used Microsoft.Azure.Documents.Client
  - For Swagger
    - dotnet add package Swashbuckle.AspNetCore -v 5.0.0
  - For KeyVault
    - dotnet add package Microsoft.Azure.Services.AppAuthentication -Version 1.4.0 (or latest)
    - dotnet add package Microsoft.Azure.KeyVault -Version 3.0.5 (or latest)
    - dotnet add package Microsoft.Extensions.Configuration.AzureKeyVault -v  2.2.0 (Use 2.2.0)
    - Version 3.1.1 and above gives a conflict with the old Microsoft.Extensions.Configuration install the latest
  - For Azure Application Insights
    - dotnet add package Microsoft.ApplicationInsights.AspNetCore -Version 2.13.1 or (latest)
    - dotnet add package Microsoft.Extensions.Logging.ApplicationInsights -Version 2.13.1 (for logging ILogger user defined logs in ApplicationInsights)

#### Note For KeyVault

"StorageAccountInformation--StorageAccountName": "Enter the StorageAccountName"
"StorageAccountInformation--StorageAccountAccessKey": "Enter the Key"
"CosmosConnectionString--CosmosEndpoint"="Enter the URI"
"CosmosConnectionString--CosmosKey"="Enter the Key"
"ApplicationInsights--InstrumentationKey"="Enter the Key"
