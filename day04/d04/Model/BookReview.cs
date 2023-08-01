using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace d04.Model
{
    public class BookReview: ISearchable
    {
        // the title of the book, its author, description, place in the rating, its (rating) title,
        // link to a page in the store.
        public string Title => Book[0].Title;
        public bool IsBest => Rank == 1;
        public class BookDetails
        {
            [JsonPropertyName("title")]
            public string Title {get; set;}
        
            [JsonPropertyName("author")]
            public string Author {get; set;}
        
            [JsonPropertyName("description")]
            public string SummaryShort {get; set;}
        
        }
        [JsonPropertyName("rank")]
        public int Rank {get; set;}
    
        [JsonPropertyName("book_details")]
        public List<BookDetails> Book {get; set; }

        [JsonPropertyName("list_name")]
        public string ListName {get; set; }

        [JsonPropertyName("amazon_product_url")]
        public string Url {get; set; }

        public override string ToString()
        {
            return $"- {Book[0].Title} by {Book[0].Author} [{Rank} on NYT’s {ListName}]\n" +
                   $"{Book[0].SummaryShort}\n" +
                   $"{Url}";
        }
    }
}

