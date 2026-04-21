using AuctionService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.Dtos
{
    public class CreateAuctionDto
    {
        public decimal ReservePrice { get; set; }                
        public DateTime AuctionEnd { get; set; }        
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
