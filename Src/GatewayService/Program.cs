using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityService"];
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
});
var app = builder.Build();
app.UseCors();
app.MapReverseProxy();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
