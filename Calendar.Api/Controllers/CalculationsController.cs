using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Data;
using Calendar.Api.Models;
using Calendar.Api.Services;
using System.Linq;

namespace Calendar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CalendarConversionService _converter;

    public CalculationsController(AppDbContext context, CalendarConversionService converter)
    {
        _context = context;
        _converter = converter;
    }

    // POST api/calculations
    [HttpPost]
    public ActionResult<IntervalCalculation> CreateCalculation([FromBody] CalculationRequest request)
    {
        var calc = new IntervalCalculation
        {
            StartDate = request.StartDate,
            IntervalType = request.IntervalType,
            IntervalValue = request.IntervalValue,
            Direction = request.Direction,
            RequestedAt = DateTime.UtcNow
        };
        _context.IntervalCalculations.Add(calc);
        _context.SaveChanges();

        for (int i = 0; i < request.IntervalValue; i++)
        {
            var offset = GetOffset(request.IntervalType, i, request.Direction);
            var date = request.StartDate.AddDays(offset);
            var conv = _converter.Convert(date);
            var result = new IntervalCalculationResult
            {
                IntervalCalculationId = calc.Id,
                StepNumber = i + 1,
                GregorianDate = conv.GregorianDate,
                JulianDate = conv.JulianDate,
                MayanLongCount = conv.MayanLongCount,
                Tzolkin = conv.Tzolkin,
                Haab = conv.Haab,
                HebrewDate = conv.HebrewDate
            };
            _context.IntervalCalculationResults.Add(result);
            calc.Results.Add(result);
        }
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetCalculation), new { id = calc.Id }, calc);
    }

    private static int GetOffset(IntervalType type, int index, CalculationDirection direction)
    {
        int days = type switch
        {
            IntervalType.Day => 1,
            IntervalType.Week => 7,
            IntervalType.Month => 30,
            _ => 1
        };
        var sign = direction == CalculationDirection.Next ? 1 : -1;
        return days * (index + 1) * sign;
    }

    // GET api/calculations/{id}
    [HttpGet("{id}")]
    public ActionResult<IntervalCalculation> GetCalculation(int id)
    {
        var calc = _context.IntervalCalculations
            .Where(c => c.Id == id)
            .FirstOrDefault();
        if (calc == null) return NotFound();
        calc.Results = _context.IntervalCalculationResults
            .Where(r => r.IntervalCalculationId == id)
            .OrderBy(r => r.StepNumber)
            .ToList();
        return calc;
    }

    // GET api/calculations
    [HttpGet]
    public IEnumerable<IntervalCalculation> GetHistory()
    {
        return _context.IntervalCalculations
            .OrderByDescending(c => c.RequestedAt)
            .ToList();
    }
}
