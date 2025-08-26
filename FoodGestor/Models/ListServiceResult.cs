using System.Text.Json.Serialization;

namespace FoodGestor.Models
{
    public class ListServiceResult<TData>
    {
        [JsonPropertyName("data")]
        public IReadOnlyCollection<TData>? Data { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("hasPrevious")]
        public bool HasPrevious { get; set; }

        [JsonPropertyName("hasNext")]
        public bool HasNext { get; set; }

        public ListServiceResult(IEnumerable<TData> data, int currentPage = 1, int pageSize = 10, int totalCount = 0) : base()
        {
            Data = new List<TData>(data).AsReadOnly();
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            HasPrevious = CurrentPage > 1;
            HasNext = CurrentPage < TotalPages;
        }
    }
}
