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

        dbContext.Contacts.Add(
          new Contact
          {
            Name = "Shimmy",
            Address = new Address
            {
              AddressI = "sdfkjasdf",
              AddressII = "sdfasdf"
            },
            Phones =
            {
              new Phone { Title = "Home", Number = "1234567890" },
              new Phone { Title = "Cell", Number = "0123456789" }
            }
          });

        await dbContext.SaveChangesAsync();
      }
    }
  }
}