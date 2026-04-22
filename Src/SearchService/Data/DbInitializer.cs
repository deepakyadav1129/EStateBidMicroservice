using Microsoft.EntityFrameworkCore;
using SearchService.RequestHelper;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(WebApplication app)
        {
            using var scope =  app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
            if (context.ItemSearches.Any()) return;
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();
            var auctions =  await httpClient.GetAuctionForSearchDB();
            if (auctions.Count() > 0)
            {
                context.ItemSearches.AddRange(auctions);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
