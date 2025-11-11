using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using competition.Models;

namespace competition.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;
      private readonly ILogger _logger;

  public CosmosDbService(
            string connectionString,
   string databaseName,
            string containerName,
   ILogger logger)
        {
            _logger = logger;
  
       var cosmosClient = new CosmosClient(connectionString);
            _container = cosmosClient.GetContainer(databaseName, containerName);
     }

        /// <summary>
        /// Obtiene todas las competiciones
        /// </summary>
        public async Task<CompetitionData> GetCompetitionsAsync()
  {
          try
            {
     // Asumiendo que tienes un solo documento con todas las competiciones
    var query = new QueryDefinition("SELECT * FROM c");
  
            var iterator = _container.GetItemQueryIterator<CompetitionData>(query);
        
                if (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
      return response.FirstOrDefault();
  }
     
     return null;
         }
 catch (CosmosException ex)
     {
    _logger.LogError(ex, "Error querying Cosmos DB");
      throw;
         }
        }

     /// <summary>
        /// Obtiene un documento por ID
     /// </summary>
  public async Task<CompetitionData> GetCompetitionDataByIdAsync(string id, string partitionKey)
   {
    try
         {
       var response = await _container.ReadItemAsync<CompetitionData>(
 id,
      new PartitionKey(partitionKey));
        
             return response.Resource;
      }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
         {
           _logger.LogWarning($"Document with id {id} not found");
   return null;
            }
         catch (CosmosException ex)
            {
          _logger.LogError(ex, "Error reading from Cosmos DB");
                throw;
            }
     }

        /// <summary>
        /// Filtra competiciones por nombre
        /// </summary>
public async Task<List<Competition>> GetCompetitionsByNameAsync(string name)
{
            try
      {
        var query = new QueryDefinition(
          "SELECT VALUE c FROM root JOIN c IN root.competitions WHERE CONTAINS(LOWER(c.name), @name)")
             .WithParameter("@name", name.ToLower());

     var iterator = _container.GetItemQueryIterator<Competition>(query);
       var results = new List<Competition>();

       while (iterator.HasMoreResults)
          {
      var response = await iterator.ReadNextAsync();
            results.AddRange(response);
              }

       return results;
       }
       catch (CosmosException ex)
            {
                _logger.LogError(ex, "Error querying competitions by name");
 throw;
            }
        }

  /// <summary>
        /// Filtra competiciones por tipo (LEAGUE, CUP, etc.)
   /// </summary>
     public async Task<List<Competition>> GetCompetitionsByTypeAsync(string type)
    {
          try
            {
     var query = new QueryDefinition(
              "SELECT VALUE c FROM root JOIN c IN root.competitions WHERE c.type = @type")
          .WithParameter("@type", type.ToUpper());

     var iterator = _container.GetItemQueryIterator<Competition>(query);
         var results = new List<Competition>();

             while (iterator.HasMoreResults)
  {
         var response = await iterator.ReadNextAsync();
   results.AddRange(response);
         }

    return results;
     }
            catch (CosmosException ex)
 {
      _logger.LogError(ex, "Error querying competitions by type");
   throw;
        }
      }

        /// <summary>
        /// Filtra competiciones por área
        /// </summary>
        public async Task<List<Competition>> GetCompetitionsByAreaAsync(string areaName)
     {
            try
    {
       var query = new QueryDefinition(
 "SELECT VALUE c FROM root JOIN c IN root.competitions WHERE CONTAINS(LOWER(c.area.name), @area)")
      .WithParameter("@area", areaName.ToLower());

    var iterator = _container.GetItemQueryIterator<Competition>(query);
  var results = new List<Competition>();

       while (iterator.HasMoreResults)
    {
         var response = await iterator.ReadNextAsync();
               results.AddRange(response);
        }

       return results;
      }
   catch (CosmosException ex)
     {
      _logger.LogError(ex, "Error querying competitions by area");
    throw;
    }
        }
    }
}
