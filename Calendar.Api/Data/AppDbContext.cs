using Microsoft.EntityFrameworkCore;
using Calendar.Api.Models;

namespace Calendar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CalendarDate> CalendarDates { get; set; }
        public DbSet<IntervalCalculation> IntervalCalculations { get; set; }
        public DbSet<IntervalCalculationResult> IntervalCalculationResults { get; set; }
    }
}
