using Microsoft.EntityFrameworkCore;
using SplitMateAPI.Models;

namespace SplitMateAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Group entity
            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupId);
                entity.Property(e => e.GroupName).IsRequired();
                
                // Store Members as JSON
                entity.Property(e => e.Members)
                    .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                        v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<string>()
                    );

                // Store Expenses as JSON
                entity.Property(e => e.Expenses)
                    .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                        v => System.Text.Json.JsonSerializer.Deserialize<List<Expense>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<Expense>()
                    );

                // Store Settlements as JSON
                entity.Property(e => e.Settlements)
                    .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                        v => System.Text.Json.JsonSerializer.Deserialize<List<Settlement>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<Settlement>()
                    );
            });
        }
    }
}
