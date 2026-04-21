using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class DbInitializer
    {

        public static void Initialize(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //SeedAuctionData(context);
        }

        private static void SeedAuctionData(ApplicationDbContext context)
        {
            context.Database.Migrate();
            if (context.Auctions.Any())
            {
                return;
            }

            var auctions = new List<Auction>()
{
    // 1 Luxury Villa
    new()
    {
        Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
        Status = AuctionStatus.Live,
        ReservePrice = 1200000,
        Seller = "bob",
        AuctionEnd = DateTime.UtcNow.AddDays(10),
        Property = new Property
        {
            Title = "Luxury Villa",
            Description = "Beautiful luxury villa with swimming pool",
            Address = "Palm Street 1",
            City = "Dubai",
            State = "Dubai",
            Bedrooms = 5,
            Bathrooms = 4,
            AreaSqFt = 5200,
            ImageUrl = "https://images.unsplash.com/photo-1613977257363-707ba9348227"
           
        }
    },

    // 2 Beach House
    new()
    {
        Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
        Status = AuctionStatus.Live,
        ReservePrice = 950000,
        Seller = "alice",
        AuctionEnd = DateTime.UtcNow.AddDays(60),
        Property = new Property
        {
            Title = "Beach House",
            Description = "Ocean facing modern beach house",
            Address = "Ocean Road 10",
            City = "Miami",
            State = "Florida",
            Bedrooms = 4,
            Bathrooms = 3,
            AreaSqFt = 3800,
            ImageUrl = "https://images.unsplash.com/photo-1502005229762-cf1b2da7c5d6"
        }
    },

    // 3 Modern Apartment
    new()
    {
        Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
        Status = AuctionStatus.Live,
        Seller = "bob",
        AuctionEnd = DateTime.UtcNow.AddDays(4),
        Property = new Property
        {
            Title = "Modern Apartment",
            Description = "City center apartment with skyline view",
            Address = "Central Ave 22",
            City = "New York",
            State = "NY",
            Bedrooms = 3,
            Bathrooms = 2,
            AreaSqFt = 1800,
            ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688"
        }
    },

    // 4 Penthouse
    new()
    {
        Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
        Status = AuctionStatus.ReserveNotMet,
        ReservePrice = 1100000,
        Seller = "tom",
        AuctionEnd = DateTime.UtcNow.AddDays(-10),
        Property = new Property
        {
            Title = "Luxury Penthouse",
            Description = "Top floor penthouse with panoramic view",
            Address = "Skyline Tower",
            City = "Chicago",
            State = "IL",
            Bedrooms = 4,
            Bathrooms = 3,
            AreaSqFt = 3200,
            ImageUrl = "https://images.unsplash.com/photo-1523217582562-09d0def993a6"
        }
    },

    // 5 Town House
    new()
    {
        Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
        Status = AuctionStatus.Live,
        ReservePrice = 550000,
        Seller = "alice",
        AuctionEnd = DateTime.UtcNow.AddDays(30),
        Property = new Property
        {
            Title = "Town House",
            Description = "Elegant townhouse in city center",
            Address = "Maple Street",
            City = "Boston",
            State = "MA",
            Bedrooms = 3,
            Bathrooms = 2,
            AreaSqFt = 2000,
            ImageUrl = "https://images.unsplash.com/photo-1568605114967-8130f3a36994"
        }
    },

    // 6 Lake House
    new()
    {
        Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
        Status = AuctionStatus.Live,
        ReservePrice = 720000,
        Seller = "bob",
        AuctionEnd = DateTime.UtcNow.AddDays(45),
        Property = new Property
        {
            Title = "Lake House",
            Description = "House with amazing lake view",
            Address = "Lakeview Road",
            City = "Michigan",
            State = "MI",
            Bedrooms = 4,
            Bathrooms = 3,
            AreaSqFt = 3500,
            ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c"
        }
    },

    // 7 Country Villa
    new()
    {
        Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
        Status = AuctionStatus.Live,
        ReservePrice = 980000,
        Seller = "alice",
        AuctionEnd = DateTime.UtcNow.AddDays(13),
        Property = new Property
        {
            Title = "Country Villa",
            Description = "Luxury countryside villa",
            Address = "Hilltop Road",
            City = "California",
            State = "CA",
            Bedrooms = 5,
            Bathrooms = 4,
            AreaSqFt = 4700,
            ImageUrl = "https://images.unsplash.com/photo-1599423300746-b62533397364"
        }
    },

    // 8 Studio Apartment
    new()
    {
        Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
        Status = AuctionStatus.Live,
        Seller = "bob",
        AuctionEnd = DateTime.UtcNow.AddDays(19),
        Property = new Property
        {
            Title = "Studio Apartment",
            Description = "Affordable studio apartment",
            Address = "Market Street",
            City = "San Francisco",
            State = "CA",
            Bedrooms = 1,
            Bathrooms = 1,
            AreaSqFt = 600,
            ImageUrl = "https://images.unsplash.com/photo-1493809842364-78817add7ffb"
        }
    },

    // 9 Family House
    new()
    {
        Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
        Status = AuctionStatus.Live,
        ReservePrice = 680000,
        Seller = "tom",
        AuctionEnd = DateTime.UtcNow.AddDays(20),
        Property = new Property
        {
            Title = "Family House",
            Description = "Perfect house for families",
            Address = "Sunset Blvd",
            City = "Los Angeles",
            State = "CA",
            Bedrooms = 4,
            Bathrooms = 3,
            AreaSqFt = 2800,
            ImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be"
        }
    },

    // 10 Smart Home
    new()
    {
        Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
        Status = AuctionStatus.Live,
        ReservePrice = 850000,
        Seller = "bob",
        AuctionEnd = DateTime.UtcNow.AddDays(48),
        Property = new Property
        {
            Title = "Smart Home",
            Description = "Fully automated smart home",
            Address = "Tech Park",
            City = "San Jose",
            State = "CA",
            Bedrooms = 4,
            Bathrooms = 3,
            AreaSqFt = 3000,
            ImageUrl = "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c"
        }
    }
};
            context.Auctions.AddRange(auctions);
            context.SaveChanges();

        }
    }
}