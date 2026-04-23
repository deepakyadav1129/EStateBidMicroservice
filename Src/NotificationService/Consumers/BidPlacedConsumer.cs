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
    public class BidPlacedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<BidPlaced>
    {
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            await hubContext.Clients.All.SendAsync("BidPlaced", context.Message);
        }
    }
}
