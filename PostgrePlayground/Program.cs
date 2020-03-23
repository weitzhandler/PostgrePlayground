using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PostgrePlayground
{
  class Program
  {
    async static Task Main(string[] args)
    {
      var host = Host
        .CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
          services.AddDbContext<AppDbContext>();
        })
        .Build();

      var services = host.Services;

      using (var dbContext = services.GetRequiredService<AppDbContext>())
      {
        await dbContext.Database.MigrateAsync();

        dbContext.SomeEntities.Add(
          new SomeEntity
          {
            Customer = new Customer
            {
              Name = "Roji",
              Age = 35,
              Orders = new[]
              {
                new Order { Price = 3, ShippingAddress = "Somewhere" },
                new Order { Price = 3, ShippingAddress = "Nowhere" }
              }
            }
          });

        await dbContext.SaveChangesAsync();
      }
    }
  }
}