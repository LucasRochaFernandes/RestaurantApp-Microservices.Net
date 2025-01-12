
using Microsoft.EntityFrameworkCore;
using RestaurantService.API.Extensions;
using RestaurantService.API.ExternalServices.RabbitMQ;
using RestaurantService.API.Infra;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adding Scoped Services
builder.Services.AddUseCaseServices();
builder.Services.AddRepositoryServices();

builder.Services.AddSingleton<IRabbitMQClient, RabbitMQClient>();

// Swagger Config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Successful Migration!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration Error: {ex.Message}");
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

