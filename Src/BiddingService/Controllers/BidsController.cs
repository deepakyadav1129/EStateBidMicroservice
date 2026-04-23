using BidingService.Data;
using BidingService.Entities;
using BidingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublishEndpoint _publisher;
        private readonly GrpcAuctionClient _client;
        public BidsController(ApplicationDbContext context, IPublishEndpoint publisher, GrpcAuctionClient client)
        {
            _context = context;
            _publisher = publisher;
            _client = client;
        }
       // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Bid>> PlaceBid(Guid auctionId,int amount)
        {
            var auction =  await _context.Auctions.FindAsync(auctionId);
            if (auction == null)
            {
                //Fetching Data from Auction Service using GRPC. if not found in bidding service database
                
                // Get Auction from Auction Service GRPC
                var auctionFromGrpc = _client.GetAuction(auctionId.ToString());
                if(auctionFromGrpc==null)
                {
                    return BadRequest("Cannot accept bid");
                }
                auction = auctionFromGrpc;
                // return NotFound();
            }
            if (User.Identity.Name == null) return Unauthorized();
            if(auction.Seller==User.Identity.Name)
            {
                return BadRequest("you cannot bid on your own property");
            }
            var bid = new Bid
            {
                Id = Guid.NewGuid(),
                AuctionId = auctionId,
                Amount = amount,
                BidTime = DateTime.Now,
                Bidder = User.Identity.Name,

            };
            if(auction.AuctionEnd<DateTime.UtcNow)
            {
                bid.BidStatus = BidStatus.Finished;
            }else
            {
                var highBid = await _context.Bids.Where(x => x.AuctionId == auctionId)
                    .OrderByDescending(x => x.Amount).FirstOrDefaultAsync();
                if (highBid != null && amount > highBid.Amount || highBid == null)
                {
                    bid.BidStatus = amount > auction.ReservePrice ? BidStatus.Accepted : BidStatus.ReserveNotMet;
                }
                if(highBid!=null && bid.Amount<=highBid.Amount)
                {
                    bid.BidStatus =  BidStatus.TooLow;

                }
               
            }
            _context.Bids.Add(bid);
            _context.SaveChanges();
             //_publisher.publish(BidPlaced);
            return Ok(bid);
        }
    }
}
