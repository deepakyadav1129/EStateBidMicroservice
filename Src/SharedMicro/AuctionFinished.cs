using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMicro
{
    public class AuctionFinished
    {
        public bool PropertySold { get; set; }
        public Guid AuctionId { get; set; }
        public string? Winner { get; set; }
        public string? Seller { get; set; }
        public int? Amount { get; set; }
    }
}
