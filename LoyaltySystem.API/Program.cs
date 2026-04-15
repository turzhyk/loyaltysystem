using LoyaltySystem.API;
using LoyaltySystem.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;

builder.Services.AddDB(configuration);
builder.Services.AddServices();
builder.Services.AddCorsPolicies();
builder.Services.AddExceptionHandler<GlobalExcepionHandler>();

var app = builder.Build();
app.UseExceptionHandler("/Error");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();