    using FoodGestor.Entitites;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System.Linq.Expressions;

    namespace FoodGestor.Context
    {
        public class FoodGestorContext : DbContext
        {
            public FoodGestorContext(DbContextOptions<FoodGestorContext> options) : base(options)
            {
            }

            public DbSet<CategoryEntity>? Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CategoryEntity>(entity =>
                {
                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(128);
                });

                foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(BaseDateTrackedEntity).IsAssignableFrom(entityType.ClrType))
                    {
                        var parameter = Expression.Parameter(entityType.ClrType, "e");
                        var deletedAtProperty = Expression.Property(parameter, nameof(BaseDateTrackedEntity.DeletedAt));
                        var comparison = Expression.Equal(deletedAtProperty, Expression.Constant(DateTime.MinValue));
                        var lambda = Expression.Lambda(comparison, parameter);

                        modelBuilder.Entity(entityType.ClrType)
                            .HasQueryFilter(lambda);
                    }
                }
            }

            public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                foreach (var entry in ChangeTracker.Entries<BaseDateTrackedEntity>())
                {
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
        }
    }
