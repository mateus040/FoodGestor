using System.Text.Json.Serialization;

namespace FoodGestor.Models;
public class CategoryModel : BaseModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
