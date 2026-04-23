using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using SharedMicro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Consumers
{
    public class AuctionCreatedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<AuctionCreated>
    {
        //where ever i have this Auction Create method in my client i will receive this message and
        //i can do whatever i want with it,
        //for example i can show a notification to the user that a new auction has been created
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            await hubContext.Clients.All.SendAsync("AuctionCreated", context.Message);
        }
    }
}
