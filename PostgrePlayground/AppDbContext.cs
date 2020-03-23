using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;

using NpgsqlTypes;

namespace PostgrePlayground
{
  public class AppDbContext : DbContext
  {
    public DbSet<SomeEntity> SomeEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Database=playground;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
  }

  public class SomeEntity
  {
    public int Id { get; set; }
    [Column(TypeName = "jsonb")]
    public Customer Customer { get; set; }
  }

  public class Customer    // Mapped to a JSON column in the table
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public Order[] Orders { get; set; }
  }

  public class Order       // Part of the JSON column
  {
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
  }
}
