using Calendar.Api.Models;

namespace Calendar.Api.Services;

public class CalendarConversionService
{
    public CalendarDate Convert(DateTime gregorian)
    {
        return new CalendarDate
        {
            GregorianDate = gregorian,
            JulianDate = ToJulianString(gregorian),
            MayanLongCount = ToMayanPlaceholder(gregorian),
            Tzolkin = ToTzolkinPlaceholder(gregorian),
            Haab = ToHaabPlaceholder(gregorian),
            CreatedAt = DateTime.UtcNow
        };
    }

    private static string ToJulianString(DateTime date)
    {
        // Simple Julian Day Number calculation
        int a = (14 - date.Month) / 12;
        int y = date.Year + 4800 - a;
        int m = date.Month + 12 * a - 3;
        int jdn = date.Day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045;
        return jdn.ToString();
    }

    private static string ToMayanPlaceholder(DateTime date)
    {
        // Placeholder conversion
        return $"MLC-{date:yyyyMMdd}";
    }

    private static string ToTzolkinPlaceholder(DateTime date)
    {
        return $"Tz-{(date.Day % 13) + 1}";
    }

    private static string ToHaabPlaceholder(DateTime date)
    {
        return $"Haab-{date.DayOfYear % 365}";
    }
}
