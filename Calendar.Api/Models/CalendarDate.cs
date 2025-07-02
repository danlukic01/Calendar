namespace Calendar.Api.Models;

public class CalendarDate
{
    public int Id { get; set; }
    public DateTime GregorianDate { get; set; }
    public string JulianDate { get; set; } = string.Empty;
    public string MayanLongCount { get; set; } = string.Empty;
    public string Tzolkin { get; set; } = string.Empty;
    public string Haab { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}
