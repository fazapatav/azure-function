using Newtonsoft.Json;
using System.Collections.Generic;

namespace competition.Models
{
    public class CompetitionData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("filters")]
        public Dictionary<string, object> Filters { get; set; }

        [JsonProperty("competitions")]
        public List<Competition> Competitions { get; set; }
    }

    public class Competition
    {
    [JsonProperty("id")]
        public int CompetitionId { get; set; }

        [JsonProperty("area")]
      public Area Area { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
   public string Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("emblem")]
   public string Emblem { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

   [JsonProperty("currentSeason")]
   public CurrentSeason CurrentSeason { get; set; }

        [JsonProperty("numberOfAvailableSeasons")]
   public int NumberOfAvailableSeasons { get; set; }

 [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
}

    public class Area
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

   [JsonProperty("code")]
     public string Code { get; set; }

 [JsonProperty("flag")]
        public string Flag { get; set; }
    }

    public class CurrentSeason
    {
        [JsonProperty("id")]
      public int Id { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

      [JsonProperty("endDate")]
      public string EndDate { get; set; }

        [JsonProperty("currentMatchday")]
        public int? CurrentMatchday { get; set; }

        [JsonProperty("winner")]
    public object Winner { get; set; }
    }
}
