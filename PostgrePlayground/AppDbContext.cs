using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
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
      //modelBuilder.Entity<Customer>().Property(se => se.Orders).HasConversion(o => o.ToArray(), o => o.ToList());

    }
  }

  public class SomeEntity
  {
    public int Id { get; set; }
    HashSet<Customer> _Customers;
    [Column(TypeName = "jsonb")]
    public HashSet<Customer> Customers
    {
      get => _Customers ??= new HashSet<Customer>();
      private set => _Customers = value;
    }
  }

  public class Customer    // Mapped to a JSON column in the table
  {
    public string Name { get; set; }
    public int Age { get; set; }
    HashSet<Order> _Orders;
    public HashSet<Order> Orders
    {
      get => _Orders ??= new HashSet<Order>();
      private set => _Orders = value;
    }

  }

  public class Order       // Part of the JSON column
  {
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
  }
}
