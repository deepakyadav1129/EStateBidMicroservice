using MassTransit;
using SearchService.Data;
using SearchService.Entities;
using SharedMicro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly ApplicationDbContext _context;
      

        public AuctionCreatedConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            
                //throw new NotImplementedException();
                var message = context.Message;
                if (_context.ItemSearches.Any(x => x.Id == message.Id)) return;
                var itemSearch = new ItemSearch
                {
                    Id = message.Id,
                    Seller = message.Seller,
                    ReservePrice = message.ReservePrice,
                    Winner = message.Winner,
                    SolAmount = message.SolAmount,
                    CurrentHighBid = message.CurrentHighBid,
                    CreatedAt = message.CreatedAt,
                    UpdatedAt = message.UpdatedAt,
                    AuctionEnd = message.AuctionEnd,
                    Title = message.Title,
                    Description = message.Description,
                    Address = message.Address,
                    City = message.City,
                    State = message.State,
                    Bedrooms = message.Bedrooms,
                    Bathrooms = message.Bathrooms,
                    AreaSqFt = message.AreaSqFt,
                    ImageUrl = message.ImageUrl
                };
                await _context.ItemSearches.AddAsync(itemSearch);
                await _context.SaveChangesAsync();
            
        }
    }
 
}
