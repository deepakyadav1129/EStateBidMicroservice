using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicro
{
    public class AuctionCreated
    {
        public Guid Id { get; set; }
        public decimal ReservePrice { get; set; }
        public string? Seller { get; set; }
        public string? Winner { get; set; }
        public decimal? SolAmount { get; set; }
        public decimal? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal AreaSqFt { get; set; }
        public string ImageUrl { get; set; }
    }
}
