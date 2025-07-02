namespace Calendar.Api.Models;

public class IntervalCalculation
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public DateTime StartDate { get; set; }
    public IntervalType IntervalType { get; set; }
    public int IntervalValue { get; set; }
    public CalculationDirection Direction { get; set; }
    public DateTime RequestedAt { get; set; }
    public ICollection<IntervalCalculationResult> Results { get; set; } = new List<IntervalCalculationResult>();
}
