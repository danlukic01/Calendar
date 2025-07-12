using Microsoft.EntityFrameworkCore;
using Calendar.Api.Data;
using Calendar.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CalendarConversionService>();
builder.Services.AddHttpClient<AflFixtureService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Apply any pending migrations. This will create the database if it does not
    // exist and update the schema when new migrations are added.
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

// Serve default static files like wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
