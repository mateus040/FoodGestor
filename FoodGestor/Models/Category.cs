using System.Text.Json.Serialization;

namespace FoodGestor.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
