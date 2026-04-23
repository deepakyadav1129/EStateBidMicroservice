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
    public class AuctionFinishedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<AuctionFinished>
    {
        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            await hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
        }
    }
}
