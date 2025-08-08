using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Api.Services;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TattslottoRulesController : ControllerBase
    {
        private readonly CalendarConversionService _converter;

        public TattslottoRulesController(CalendarConversionService converter)
        {
            _converter = converter;
        }

        private static readonly Dictionary<int, int> DateMatrix = new()
        {
             {1,1}, {2,2}, {3,3}, {4,4}, {5,5}, {6,6}, {7,7}, {8,8}, {9,9},
              {10,10}, {11,11}, {12,12}, {13,13}, {14,14}, {15,15}, {16,16}, {17,17}, {18,18},
              {19,19}, {20,20}, {21,22}, {22,22}, {23,23}, {24,24}, {25,25}, {26,26},
              {27,27}, {28,28}, {29,29}, {30,30}, {31,31},
        };

        [HttpGet]
        public ActionResult<object> Get([FromQuery] int day, [FromQuery] int month)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12)
            {
                return BadRequest("Invalid day or month");
            }

            int r16 = Rule16(day, month, DateMatrix);

            return Ok(new { rule16 = r16 });
        }

        [HttpGet("system126")]
        public ActionResult<object> GetSystem126([FromQuery] DateTime? date)
        {
            DateTime baseDate = (date ?? DateTime.Today).Date;

            int SumDigits(int value) => value.ToString().Sum(c => int.Parse(c.ToString()));
            int SumDateDigits(DateTime d) => SumDigits(d.Day) + SumDigits(d.Month) + SumDigits(d.Year);

            object Build(DateTime d)
            {
                var conv = _converter.Convert(d);
                return new
                {
                    gregorianDate = conv.GregorianDate.ToString("yyyy-MM-dd"),
                    digitSum = SumDateDigits(d),
                    julianDate = conv.JulianDate,
                    mayanLongCount = conv.MayanLongCount,
                    tzolkin = conv.Tzolkin,
                    haab = conv.Haab,
                    hebrewDate = conv.HebrewDate
                };
            }

            var days = Build(baseDate.AddDays(126));
            var months = Build(baseDate.AddMonths(126));
            var weeks = Build(baseDate.AddDays(126 * 7));
            var years = Build(baseDate.AddYears(126));
            var combined = Build(baseDate.AddYears(126).AddMonths(126).AddDays(126));

            return Ok(new { days, months, weeks, years, combined });
        }

        private static int Rule16(int day, int month, Dictionary<int, int> matrix)
        {
            int sumDigits(int value) => value.ToString().Select(c => int.Parse(c.ToString())).Sum();

            return sumDigits(day) + sumDigits(matrix[day]) +
                   sumDigits(month) + sumDigits(matrix[month]);
        }
    }
}

