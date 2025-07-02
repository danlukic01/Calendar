namespace Calendar.Api.Models;

public class CalculationRequest
{
    public DateTime StartDate { get; set; }
    public IntervalType IntervalType { get; set; }
    public int IntervalValue { get; set; }
    public CalculationDirection Direction { get; set; }
}
