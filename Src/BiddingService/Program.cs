using BidingService.Consumers;
using BidingService.Data;
using BidingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(busConfig =>
{
    busConfig.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("mybus-bids", false));
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


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityService"];
        options.TokenValidationParameters.NameClaimType = "username";
        options.TokenValidationParameters.ValidateAudience = false;
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Admin"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<GrpcAuctionClient>();

builder.Services.AddHostedService<AuctionFinishedCheck>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
