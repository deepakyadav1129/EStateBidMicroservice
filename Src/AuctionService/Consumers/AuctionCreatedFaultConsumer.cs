using MassTransit;
using SharedMicro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionService.Consumers
{
    public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
        {
            //await context.Publish(context.Message.Message);
           var id =   context.Message.Message.Id;
            // var auction =  context.auction.find(id);
            // auction.status =  "failed";
            // savechanges();
        }
    }
}
