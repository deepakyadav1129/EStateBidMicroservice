using BidingService.Data;
using BidingService.Entities;
using MassTransit;
using SharedMicro;

namespace BidingService.Consumers
{
    //Here bidding service is consuming the AuctionCreated event from auction
        // just like we are doing in the search service.
    public class AuctionCreatedConsumer(ApplicationDbContext _context) : IConsumer<AuctionCreated>
    {
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            var message = context.Message;
            var auction = new Auction
            {
                Id = message.Id,
                AuctionEnd = message.AuctionEnd,
                Seller = message.Seller,
                ReservePrice = message.ReservePrice,
                IsFinished = false
            };
           await  _context.Auctions.AddAsync(auction);
            await _context.SaveChangesAsync();
        }
    }
}
