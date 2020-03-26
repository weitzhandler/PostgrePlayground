using System.Diagnostics;
using System.Linq;
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

      using (var context = services.GetRequiredService<AppDbContext>())
      {
        var db = context.Database;
        await db.EnsureDeletedAsync();
        await db.EnsureCreatedAsync();


        context.Parents.Add(new Parent { WrappedText = new WrappedText("Hello") });


        await context.SaveChangesAsync();

        context.Parents.Local.Clear();
        var parents = await context.Parents.Where(p => p.WrappedText == "Hello").ToListAsync();

        Debugger.Break();

        db.EnsureDeleted();
      }



    }
  }
}