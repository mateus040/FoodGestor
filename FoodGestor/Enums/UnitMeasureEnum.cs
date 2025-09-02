using System.Text.Json.Serialization;

namespace FoodGestor.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UnitMeasureEnum
    {
        Grams,
        Kilograms,
        Milliliters,
        Liters,
        Units,
    }
}
