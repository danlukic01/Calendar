using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Services;
using Calendar.Api.Models;

namespace Calendar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DateController : ControllerBase
{
    private readonly CalendarConversionService _converter;

    public DateController(CalendarConversionService converter)
    {
        _converter = converter;
    }

    // GET api/date/current
    [HttpGet("current")]
    public ActionResult<CalendarDate> GetCurrent()
    {
        var conv = _converter.Convert(DateTime.UtcNow);
        return conv;
    }

    // GET api/date/convert?date=yyyy-MM-dd
    [HttpGet("convert")]
    public ActionResult<CalendarDate> ConvertDate([FromQuery] DateTime date)
    {
        var conv = _converter.Convert(date);
        return conv;
    }
}
