using System.Text.Json.Serialization;

namespace FoodGestor.Models
{
    public abstract class BaseModel
    {
        private DateTime _createdAt = DateTime.UtcNow;
        private DateTime _updatedAt = DateTime.UtcNow;

        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt
        {
            get => new DateTimeOffset(_createdAt).ToOffset(new TimeSpan(-3, 0, 0));
            set => _createdAt = value.UtcDateTime;
        }

        [JsonPropertyName("updatedAt")]
        public DateTimeOffset UpdatedAt
        {
            get => new DateTimeOffset(_updatedAt).ToOffset(new TimeSpan(-3, 0, 0));
            set => _updatedAt = value.UtcDateTime;
        }
    }
}
