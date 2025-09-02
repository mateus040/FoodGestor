using FoodGestor.Enums;

namespace FoodGestor.Entitites
{
    public class IngredientEntity : BaseDateTrackedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UnitMeasureEnum UnitMeasure { get; set; }
        public int Quantity { get; set; }
    }
}
