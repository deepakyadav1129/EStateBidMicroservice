using AuctionService.Data;
using Grpc.Core;

namespace AuctionService.Services
{
    public class GrpcAuctionService(ApplicationDbContext _context) :  GrpcAuction.GrpcAuctionBase
    {
        public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
        {
            Console.WriteLine("Received Message on Auction Server");
            var auction = await _context.Auctions.FindAsync(Guid.Parse(request.Id))
                ?? throw new RpcException(new Status(StatusCode.NotFound,"NotFound"));
            var response = new GrpcAuctionResponse
            {
                Auction = new GrpcAuctionModel
                {
                    Id = request.Id.ToString(),
                    Seller = auction.Seller,
                    ReservePrice = Convert.ToInt32(auction.ReservePrice),
                    AuctionEnd = auction.AuctionEnd.ToString(),
                }
            };
            return response;

        }
    }
}
