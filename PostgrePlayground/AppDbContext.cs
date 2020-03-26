using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NpgsqlTypes;

namespace PostgrePlayground
{
  public class AppDbContext : DbContext
  {
    public DbSet<Parent> Parents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql(@"Host=127.0.0.1;Database=playground;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder
        .Entity<Parent>()
        .Property(p =>
          p.WrappedText)
        .HasConversion(new WrappedText.WrappedTextValueConverter());


    }
  }

  public class Parent
  {
    public int Id { get; set; }

    public WrappedText WrappedText { get; set; }
  }


  public class WrappedText
  {
    public WrappedText(string value)
    {
      Value = value;
    }

    public string Value { get; private set; }

    public static implicit operator string(WrappedText t) => t.Value;
    public static implicit operator WrappedText(string value) => new WrappedText(value);

    public class WrappedTextValueConverter : ValueConverter<WrappedText, string>
    {
      public WrappedTextValueConverter()
        : base(wt => wt, value => value)
      {
      }
    }
  }
}
