using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicro
{
    public class BidPlaced
    {
        public required Guid Id { get; set; }
        public required Guid AuctionId { get; set; }
        public required string  Bidder { get; set; }
        public DateTime  BidTime { get; set; }
        public int  Amount { get; set; }
        public required string  BidStatus { get; set; }

    }
}
