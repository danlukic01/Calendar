using Microsoft.EntityFrameworkCore;
using Calendar.Api.Data;
using Calendar.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CalendarConversionService>();

builder.Services.AddDbContextFactory<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped // recommended
);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

// Serve default static files like wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
