using System.Collections.Generic;
using System.Text.Json.Serialization;
using d04.Model;

namespace d04.Model{}

public class MovieReview : ISearchable
{
    [JsonPropertyName("title")]
    public string Title {get; set;}
    public bool IsBest => IsCriticsPick == 1;
        
    [JsonPropertyName("mpaa_rating")]
    public string Rate {get; set;}
    
    [JsonPropertyName("critics_pick")]
    public int IsCriticsPick  {get; set;}
        
    [JsonPropertyName("summary_short")]
    public string SummaryShort {get; set;}

    [JsonPropertyName("link")]
    public PageLink Link { get; set; }
    public class PageLink
    {
        [JsonPropertyName("url")]
        public string Url {get; set; }
    }

    public override string ToString()
    {
        return $"- {Title.ToUpper()}{(IsCriticsPick == 1 ? " [NYT critic’s pick]" : "") }\n" +
                $"{SummaryShort}\n{Link.Url}";
    }
}