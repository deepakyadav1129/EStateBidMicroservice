
using MassTransit;
using SearchService.Data;
using SharedMicro;

namespace SearchService.Consumers
{
    public class BidPlacedConsumer(ApplicationDbContext _context) : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            var auction = await _context.ItemSearches.FindAsync(context.Message.AuctionId);
            if(auction!=null && (auction.CurrentHighBid==null
                || context.Message.BidStatus.Contains("Accepted") && context.Message.Amount>auction.CurrentHighBid))
            {
                auction.CurrentHighBid = context.Message.Amount;
                await _context.SaveChangesAsync();
            }

        }
    }
}
