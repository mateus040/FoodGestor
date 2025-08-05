using FoodGestor.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodGestor.Context
{
    public class FoodGestorContext : DbContext
    {
        public FoodGestorContext(DbContextOptions<FoodGestorContext> options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
    }
}
