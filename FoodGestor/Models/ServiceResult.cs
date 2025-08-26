using System.Text.Json.Serialization;

namespace FoodGestor.Models
{
    public class ServiceResult<TData>
    {
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData? Data { get; set; }
        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        public ServiceResult(TData data)
        {
            Data = data;
        }

        public ServiceResult(string message)
        {
            Message = message;
        }
    }
}
