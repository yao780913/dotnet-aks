using Microsoft.EntityFrameworkCore;
using Shopping.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(Environment.GetEnvironmentVariable("ConnStr"));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
try
{
    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}
catch (Exception e)
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, e.Message);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();