using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using competition.Services;
using competition.Models;
using System.Linq;

namespace competition
{
    public static class Function1
    {

        //[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        [FunctionName("GetCompetitions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing request to get competitions from Cosmos DB.");

            try
            {

                // Obtener configuración desde variables de entorno
                var connectionString = "AccountEndpoint=https://poli.documents.azure.com:443/;AccountKey=xxx;";
                var databaseName = "competitions";
                var containerName = "football";

                // Validar configuración
                if (string.IsNullOrEmpty(connectionString) ||
                    string.IsNullOrEmpty(databaseName) ||
                    string.IsNullOrEmpty(containerName))
                {
                    log.LogError("Cosmos DB configuration is missing");
                    return new BadRequestObjectResult(new
                    {
                        error = "Cosmos DB configuration is not properly set up"
                    });
                }

                // Crear servicio de Cosmos DB
                var cosmosService = new CosmosDbService(
                    connectionString,
                    databaseName,
                    containerName,
                    log);

                // Obtener parámetros de la query
                string name = req.Query["name"];
                string type = req.Query["type"];
                string area = req.Query["area"];
                string documentId = req.Query["id"];

                // Si se proporciona un ID específico, obtener ese documento
                if (!string.IsNullOrEmpty(documentId))
                {
                    var partitionKey = req.Query["partitionKey"];
                    if (string.IsNullOrEmpty(partitionKey))
                    {
                        partitionKey = documentId; // Usar el ID como partition key por defecto
                    }

                    var competitionData = await cosmosService.GetCompetitionDataByIdAsync(documentId, partitionKey);

                    if (competitionData == null)
                    {
                        return new NotFoundObjectResult(new { error = $"Document with id '{documentId}' not found" });
                    }

                    return new OkObjectResult(competitionData);
                }

                // Filtrar por nombre
                if (!string.IsNullOrEmpty(name))
                {
                    var competitions = await cosmosService.GetCompetitionsByNameAsync(name);
                    return new OkObjectResult(new
                    {
                        count = competitions.Count,
                        competitions = competitions
                    });
                }

                // Filtrar por tipo
                if (!string.IsNullOrEmpty(type))
                {
                    var competitions = await cosmosService.GetCompetitionsByTypeAsync(type);
                    return new OkObjectResult(new
                    {
                        count = competitions.Count,
                        competitions = competitions
                    });
                }

                // Filtrar por área
                if (!string.IsNullOrEmpty(area))
                {
                    var competitions = await cosmosService.GetCompetitionsByAreaAsync(area);
                    return new OkObjectResult(new
                    {
                        count = competitions.Count,
                        competitions = competitions
                    });
                }

                // Si no hay filtros, obtener todas las competiciones
                var allCompetitions = await cosmosService.GetCompetitionsAsync();

                if (allCompetitions == null)
                {
                    return new OkObjectResult(new
                    {
                        count = 0,
                        competitions = new object[0]
                    });
                }

                return new OkObjectResult(allCompetitions);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error processing request");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
