using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerballRulesController : ControllerBase
    {
        [HttpGet("extendedsuperrule6")]
        public ActionResult<object> GetExtendedSuperRule6()
        {
            DateTime today = DateTime.Today;
            int offset = -126;

            DateTime gregDate = today.AddDays(offset);
            int gDay = gregDate.Day;

            var hebrewCal = new HebrewCalendar();
            DateTime hebrewDate = hebrewCal.AddDays(today, offset);
            int hDay = hebrewCal.GetDayOfMonth(hebrewDate);

            var julianCal = new JulianCalendar();
            DateTime julianDate = julianCal.AddDays(today, offset);
            int jDay = julianCal.GetDayOfMonth(julianDate);

            int gPlusH = gDay + hDay;
            int gPlusJ = gDay + jDay;
            int hPlusJ = hDay + jDay;
            int gPlusHPlusJ = gDay + hDay + jDay;
            int gPlusHPlusJPlus1 = gPlusHPlusJ + 1;

            int gPlus9 = gDay + 9;
            int hPlus9 = hDay + 9;
            int jPlus9 = jDay + 9;

            return Ok(new
            {
                gregorianDay = gDay,
                hebrewDay = hDay,
                julianDay = jDay,
                gregorianPlusHebrew = gPlusH,
                gregorianPlusJulian = gPlusJ,
                hebrewPlusJulian = hPlusJ,
                gregorianPlusHebrewPlusJulian = gPlusHPlusJ,
                gregorianPlusHebrewPlusJulianPlusOne = gPlusHPlusJPlus1,
                gregorianPlus9 = gPlus9,
                hebrewPlus9 = hPlus9,
                julianPlus9 = jPlus9
            });
        }
    }
}
