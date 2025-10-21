namespace FoodGestor.Entitites
{
    public abstract class BaseDateTrackedEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.MinValue;
    }
}
