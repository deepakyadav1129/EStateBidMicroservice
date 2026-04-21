using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public decimal ReservePrice { get; set; }
        public required string Seller { get; set; }
        public string? Winner { get; set; }
        public decimal? SolAmount { get; set; }
        public decimal? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime AuctionEnd { get; set; }
        public AuctionStatus Status { get; set; }
        public required Property Property { get; set; }

    }
}
