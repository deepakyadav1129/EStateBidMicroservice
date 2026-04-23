using BidingService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedMicro;

namespace BidingService.Services
{
    public class AuctionFinishedCheck : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AuctionFinishedCheck(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          while(!stoppingToken.IsCancellationRequested)
            {
                await checkAuction(stoppingToken);
                await Task.Delay(5000,stoppingToken);
            }
        }

        private async Task checkAuction(CancellationToken stoppingToken)
        {
            var now =  DateTime.UtcNow;
            using var scope = _serviceProvider.CreateScope(); 
            var context =  scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var publisher =  scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();


            var auctionFinished = await  context.Auctions.Where
                (x=>x.AuctionEnd<=now && !x.IsFinished).ToListAsync(stoppingToken);
            if (!auctionFinished.Any()) return;
            foreach (var auction in auctionFinished)
            {
                auction.IsFinished = true;
                var winingBid = await context.Bids.Where(x => x.AuctionId == auction.Id &&
                x.BidStatus == Entities.BidStatus.Accepted).OrderByDescending(x=>x.Amount).FirstOrDefaultAsync(stoppingToken);
                // publish to RabbitMQ

                await publisher.Publish(new AuctionFinished
                {
                    AuctionId = auction.Id,
                    PropertySold = winingBid != null,
                    Winner = winingBid?.Bidder,
                    Amount = winingBid?.Amount,
                    Seller = auction.Seller

                }, stoppingToken);

            }

         await context.SaveChangesAsync();  
        }
    }
}
