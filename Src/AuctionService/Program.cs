using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddMassTransit(busConfig =>
{
    // Register abstraction services like Buses :  IBus, IBusControl, IPublishAsync, ISendMessageProvider 
    busConfig.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();

    //Input / output paterna messages ko store krne ke liye outbox pattern use kr rhe hai
    //we are passing the data into the outbox entity first just in case if search service is down or any other issue
    //then we can retry to send the message to the bus and we will not lose any data because it is stored in the outbox table
    //in the database and we can retry to send the message to the bus until it is successful.
    busConfig.AddEntityFrameworkOutbox<ApplicationDbContext>(outBox =>
    {
        outBox.QueryDelay = TimeSpan.FromSeconds(10);
        outBox.UseSqlServer();
        outBox.UseBusOutbox();
    });

    busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("mybus-auction", false));

    busConfig.UsingRabbitMq((busContext, factoryConfig) =>
    {

        //factoryConfig.ReceiveEndpoint("auction-created", rconfig =>
        //{
        //    rconfig.UseMessageRetry(r => r.Interval(5, 5));
        //    rconfig.ConfigureConsumer<AuctionCreatedConsumer>(busContext);
        //});

        // _transport = new RabbitMqRegistration(factoryConfig)
        //var bus =  _transport.CreateBus(); IBusController :  IBus-- StartAsync , StopAsync
        factoryConfig.ConfigureEndpoints(busContext);
    });


});




// Add services to the container.

builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//DbInitializer.Initialize(app);
app.Run();
