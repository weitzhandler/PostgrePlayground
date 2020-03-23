using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using NpgsqlTypes;

namespace PostgrePlayground
{
  public class AppDbContext : DbContext
  {
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Database=playground;Password=Sf_@dG3fm)+2");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<Contact>(contact =>
        {
          contact
          .Property(c => c.Address)
          .HasColumnType("jsonb");

          contact
          .Property(c => c.Phones)
          .HasColumnType("jsonb");
        });
    }
  }

  public class Contact
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [Column(TypeName = nameof(NpgsqlDbType.Jsonb))]
    public Address Address { get; set; }

    HashSet<Phone> _Phones;
    [Column(TypeName = nameof(NpgsqlDbType.Jsonb))]
    public HashSet<Phone> Phones
    {
      get => _Phones ??= new HashSet<Phone>();
      set => _Phones = value;
    }
  }

  public class Phone
  {
    public string Title { get; set; }
    public string Number { get; set; }

  }

  public class Address
  {
    public string AddressI { get; set; }
    public string AddressII { get; set; }
  }
}
