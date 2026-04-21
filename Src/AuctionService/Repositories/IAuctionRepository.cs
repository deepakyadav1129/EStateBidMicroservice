using AuctionService.Dtos;
using AuctionService.Entities;

namespace AuctionService.Repositories
{
    public interface IAuctionRepository
    {
        Task<List<AuctionDto>> GetAllAsync();
        Task<AuctionDto?> GetByIdAsync(Guid id);
        Task<Auction?> GetAuctionByIdAsync(Guid id);
        void AddAuction(Auction auction);
        void RemoveAuction(Auction auction);
        Task<bool> SaveChangesAsync();
    }
}
