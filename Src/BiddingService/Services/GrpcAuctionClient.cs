using AuctionService;
using BidingService.Entities;
using Grpc.Net.Client;

namespace BidingService.Services
{
    public class GrpcAuctionClient
    {
        private readonly IConfiguration _configuration;

        public GrpcAuctionClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Auction GetAuction(string id)
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcAuctionAddress"]!);
            var client = new GrpcAuction.GrpcAuctionClient(channel);
            var request =  new GetAuctionRequest { Id = id };
            try
            {
                var response = client.GetAuction(request);
                var auction = new Auction
                {
                    Id = Guid.Parse(response.Auction.Id),
                    AuctionEnd = DateTime.Parse(response.Auction.AuctionEnd),
                    Seller = response.Auction.Seller,
                    ReservePrice = response.Auction.ReservePrice,

                };
                return auction;
            }
            catch (Exception)
            {
                Console.WriteLine("could not connect");
                return null;
            }
         

        }

    }
}
