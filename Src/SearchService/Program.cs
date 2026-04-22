using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.RequestHelper;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());
// Add services to the container.

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

//The purpose the adding this code instead of directly calling the DbInitializer.Initialize(app)
//is to handle the case when the AuctionService is not up and running yet, which can cause a TimeoutException.
//By using Polly's retry policy, we can attempt to initialize the database multiple times with a delay in between,
//increasing the chances of successful initialization once the AuctionService is available.
//also in this case the application will not crash and will keep trying until the AuctionService is up and running,
//ensuring that the database is initialized properly without manual intervention.
app.Lifetime.ApplicationStarted.Register(async () =>
{
    await Policy.Handle<TimeoutException>()
    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(5))
    .ExecuteAndCaptureAsync(async () => await DbInitializer.Initialize(app));
});

//when we were running the application for the first time, there is a chance that the AuctionService is not up and running yet,
//If the initialization fails after 5 attempts, after trying servertime the applicaion will crash and the no operation will happen.
//await DbInitializer.Initialize(app);

app.Run();

// Polly policy to handle transient errors and retry after a delay
static IAsyncPolicy<HttpResponseMessage> GetPolicy() =>
    HttpPolicyExtensions.HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));
