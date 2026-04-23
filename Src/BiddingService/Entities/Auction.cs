using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingService.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public DateTime AuctionEnd { get; set; }
        public required string Seller { get; set; }
        public required decimal ReservePrice { get; set; }
        public bool IsFinished { get; set; }
    }
}
