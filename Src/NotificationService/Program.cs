using MassTransit;
using NotificationService.Consumers;
using NotificationService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(busConfig =>
{
    busConfig.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("notify", false));
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

builder.Services.AddSignalR();

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
app.MapHub<NotificationHub>("/notifications");
app.Run();
