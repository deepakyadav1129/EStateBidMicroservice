using AuctionService.Data;
using AuctionService.Dtos;
using AuctionService.Entities;
using AuctionService.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
        }

        public async Task<List<AuctionDto>> GetAllAsync()
        {
            var auctions = await _context.Auctions.Include(x => x.Property).ToListAsync();
            return auctions.Select(x => x.ToAuctionDto()).ToList();
        }

        public async Task<Auction?> GetAuctionByIdAsync(Guid id)
        {
            return await _context.Auctions.Include(x => x.Property).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AuctionDto?> GetByIdAsync(Guid id)
        {
            var auction = await _context.Auctions.Include(x => x.Property).FirstOrDefaultAsync(x => x.Id == id);
            return auction?.ToAuctionDto();
        }

        public void RemoveAuction(Auction auction)
        {
            _context.Auctions.Remove(auction);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
