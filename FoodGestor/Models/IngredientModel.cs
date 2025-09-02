using FoodGestor.Enums;
using System.Text.Json.Serialization;

namespace FoodGestor.Models
{
    public class IngredientModel : BaseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unit_measure")]
        public UnitMeasureEnum UnitMeasure { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
