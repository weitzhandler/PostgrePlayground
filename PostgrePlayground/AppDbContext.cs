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
  }

  public class Contact
  {
    public int Id { get; set; }

    public string Name { get; set; }

    [Column(TypeName = "jsonb")]
    public Address Address { get; set; }

    [Column(TypeName = "jsonb")]
    public Phone[] Phones { get; set; }
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
