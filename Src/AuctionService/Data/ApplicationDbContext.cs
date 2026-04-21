using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Property> Properties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Property>().ToTable("Properties");
            modelBuilder.Entity<Auction>()
                .HasOne(x => x.Property)
                .WithOne(x => x.Auction)
                .HasForeignKey<Property>(x => x.AuctionId);
            //modelBuilder.AddOutboxMessageEntity();
            //modelBuilder.AddOutboxStateEntity();
        }
    }
}
