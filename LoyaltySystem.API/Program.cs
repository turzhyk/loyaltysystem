using LoyaltySystem.API;
using LoyaltySystem.API.Extensions;
using LoyaltySystem.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;

builder.Services.AddDb(configuration);
builder.Services.AddServices();
builder.Services.AddCorsPolicies();
builder.Services.AddExceptionHandler<GlobalExcepionHandler>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DiscountSeeder>();
    await seeder.Seed();
    var seeder2 = scope.ServiceProvider.GetRequiredService<UserSeeder>();
    await seeder2.Seed();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();