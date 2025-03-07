using LimsOutillageService.Data;
using Microsoft.EntityFrameworkCore;
using LimsOutillageService.Services;
var builder = WebApplication.CreateBuilder(args);

// Ajouter le service DbContext
builder.Services.AddDbContext<OutillageContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Enregistre le service OutillagService
builder.Services.AddScoped<IOutillageService, OutillageService>();


// Enregistre le service OutillagService
builder.Services.AddScoped<IMarqueService, MarqueService>();

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
