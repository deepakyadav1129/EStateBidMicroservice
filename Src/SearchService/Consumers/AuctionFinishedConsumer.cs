using MassTransit;
using SearchService.Data;
using SharedMicro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Consumers
{
    public class AuctionFinishedConsumer(ApplicationDbContext _context) : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auction = await _context.ItemSearches.FindAsync(context.Message.AuctionId);
            if (auction == null) return;
            if (context.Message.PropertySold)
            {
                auction.Winner = context.Message.Winner;
                auction.SolAmount = context.Message.Amount;

            }
            //auction.Status = auction.SolAmount > auction.ReservePrice ?
            //    Entities.AuctionStatus.Finished : Entities.AuctionStatus.ReserveNotMet;
            await _context.SaveChangesAsync();


        }
    }
}
