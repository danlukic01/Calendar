namespace Calendar.Api.Models;

public class IntervalCalculationResult
{
    public int Id { get; set; }
    public int IntervalCalculationId { get; set; }
    public int StepNumber { get; set; }
    public DateTime GregorianDate { get; set; }
    public string JulianDate { get; set; } = string.Empty;
    public string MayanLongCount { get; set; } = string.Empty;
    public string Tzolkin { get; set; } = string.Empty;
    public string Haab { get; set; } = string.Empty;
    public string HebrewDate { get; set; } = string.Empty;
}
