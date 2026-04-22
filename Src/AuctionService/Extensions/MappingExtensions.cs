using AuctionService.Dtos;
using AuctionService.Entities;
using SharedMicro;

namespace AuctionService.Extensions
{
    public static class MappingExtensions
    {
        public static AuctionDto ToAuctionDto(this Auction auction)
        {
            return new AuctionDto
            {
                Id = auction.Id,
                ReservePrice = auction.ReservePrice,
                Seller = auction.Seller,
                Winner = auction.Winner,
                SolAmount = auction.SolAmount,
                CurrentHighBid = auction.CurrentHighBid,
                CreatedAt = auction.CreatedAt,
                UpdatedAt = auction.UpdatedAt,
                AuctionEnd = auction.AuctionEnd,
                Status = auction.Status,
                Title = auction.Property.Title,
                Description = auction.Property.Description,
                Address = auction.Property.Address,
                City = auction.Property.City,
                State = auction.Property.State,
                Bedrooms = auction.Property.Bedrooms,
                Bathrooms = auction.Property.Bedrooms,
                AreaSqFt = auction.Property.AreaSqFt,
                ImageUrl = auction.Property.ImageUrl
            };
        }
        public static Auction ToAuctionEntity(this CreateAuctionDto createAuctionDto)
        {
            return new Auction
            {

                ReservePrice = createAuctionDto.ReservePrice,
                AuctionEnd = createAuctionDto.AuctionEnd,
                Seller = "demo",
                Status = AuctionStatus.Live,
                Property = new Property
                {
                    Title = createAuctionDto.Title,
                    Description = createAuctionDto.Description,
                    Address = createAuctionDto.Address,
                    City = createAuctionDto.City,
                    State = createAuctionDto.State,
                    Bedrooms = createAuctionDto.Bedrooms,
                    Bathrooms = createAuctionDto.Bedrooms,
                    AreaSqFt = createAuctionDto.AreaSqFt,
                    ImageUrl = createAuctionDto.ImageUrl,
                    StartingPrice = createAuctionDto.ReservePrice
                }



            };
        }
        public static void ToUpdateAuction(this Auction auction,UpdateAuctionDto dto)
        {
            auction.UpdatedAt = DateTime.UtcNow;
            auction.Property.Title =  dto.Title??auction.Property.Title;    
            auction.Property.Description =  dto.Description??auction.Property.Description;    
            auction.Property.Address =  dto.Address??auction.Property.Address;    
            auction.Property.City =  dto.City??auction.Property.City;    
            auction.Property.State =  dto.State??auction.Property.State;            
            auction.Property.Bedrooms =  dto.Bedrooms; 
            auction.Property.Bathrooms =  dto.Bathrooms; 
            auction.Property.AreaSqFt =  dto.AreaSqFt; 
            auction.Property.ImageUrl =  dto.ImageUrl??string.Empty; 
            
        }
        public static AuctionCreated ToAuctionCreated(this Auction auction)
        {
            return new AuctionCreated
            {
                Id = auction.Id,
                ReservePrice = auction.ReservePrice,
                Seller = auction.Seller,
                Winner = auction.Winner,
                SolAmount = auction.SolAmount,
                CurrentHighBid = auction.CurrentHighBid,
                CreatedAt = auction.CreatedAt,
                UpdatedAt = auction.UpdatedAt,
                AuctionEnd = auction.AuctionEnd,
                Status = auction.Status.ToString(),
                Title = auction.Property.Title,
                Description = auction.Property.Description,
                Address = auction.Property.Address,
                City = auction.Property.City,
                State = auction.Property.State,
                Bedrooms = auction.Property.Bedrooms,
                Bathrooms = auction.Property.Bathrooms,
                AreaSqFt = auction.Property.AreaSqFt,
                ImageUrl = auction.Property.ImageUrl

            };
        }
    }
}
