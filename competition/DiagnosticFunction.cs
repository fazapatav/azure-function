using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace competition
{
    public static class DiagnosticFunction
    {
        [FunctionName("DiagnosticCosmosDb")]
     public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
        {
       log.LogInformation("Running Cosmos DB diagnostic...");

      try
            {
         var connectionString = Environment.GetEnvironmentVariable("CosmosDbConnectionString");
         
    if (string.IsNullOrEmpty(connectionString))
         {
return new BadRequestObjectResult(new { error = "CosmosDbConnectionString not configured" });
  }

    var cosmosClient = new CosmosClient(connectionString);

// Listar todas las bases de datos
     var databasesIterator = cosmosClient.GetDatabaseQueryIterator<DatabaseProperties>();
         var databases = new System.Collections.Generic.List<string>();

     while (databasesIterator.HasMoreResults)
             {
     var response = await databasesIterator.ReadNextAsync();
                databases.AddRange(response.Select(db => db.Id));
        }

        log.LogInformation($"Found {databases.Count} database(s)");

  // Intentar listar contenedores para cada base de datos
  var databaseInfo = new System.Collections.Generic.List<object>();

   foreach (var dbName in databases)
        {
           try
           {
               var database = cosmosClient.GetDatabase(dbName);
        var containersIterator = database.GetContainerQueryIterator<ContainerProperties>();
          var containers = new System.Collections.Generic.List<string>();

        while (containersIterator.HasMoreResults)
           {
  var response = await containersIterator.ReadNextAsync();
               containers.AddRange(response.Select(c => c.Id));
            }

          databaseInfo.Add(new
               {
            database = dbName,
       containers = containers,
          containerCount = containers.Count
    });
        }
         catch (Exception ex)
         {
          log.LogError(ex, $"Error listing containers for database {dbName}");
             databaseInfo.Add(new
      {
         database = dbName,
                error = ex.Message
            });
     }
  }

        var result = new
       {
       accountEndpoint = cosmosClient.Endpoint.ToString(),
       databaseCount = databases.Count,
      databases = databaseInfo,
        configuredDatabase = Environment.GetEnvironmentVariable("CosmosDbDatabaseName"),
            configuredContainer = Environment.GetEnvironmentVariable("CosmosDbContainerName")
     };

     return new OkObjectResult(result);
}
            catch (Exception ex)
      {
      log.LogError(ex, "Error running diagnostic");
         return new ObjectResult(new
         {
   error = ex.Message,
      stackTrace = ex.StackTrace
        })
        {
  StatusCode = 500
        };
       }
        }
    }
}
