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
            MayanLongCount = ToMayanLongCount(gregorian),
            Tzolkin = ToTzolkin(gregorian),
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

    private static int JulianDayNumber(DateTime date)
    {
        int a = (14 - date.Month) / 12;
        int y = date.Year + 4800 - a;
        int m = date.Month + 12 * a - 3;
        return date.Day + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045;
    }

    private static string ToMayanLongCount(DateTime date)
    {
        int jdn = JulianDayNumber(date);
        int days = jdn - 584283; // GMT correlation constant
        int baktun = days / 144000;
        int katun = (days % 144000) / 7200;
        int tun = (days % 7200) / 360;
        int uinal = (days % 360) / 20;
        int kin = days % 20;
        return $"{baktun}.{katun}.{tun}.{uinal}.{kin}";
    }

    private static readonly string[] TzolkinNames = new[]
    {
        "Imix", "Ik'", "Ak'b'al", "K'an", "Chikchan",
        "Kimi", "Manik'", "Lamat", "Muluk", "Ok",
        "Chuwen", "Eb'", "Ben", "Ix", "Men",
        "Kib'", "Kab'an", "Etz'nab'", "Kawak", "Ajaw"
    };

    private static string ToTzolkin(DateTime date)
    {
        int jdn = JulianDayNumber(date);
        int days = jdn - 584283;
        int number = ((days + 3) % 13) + 1; // 0.0.0.0.0 = 4 Ajaw
        int nameIndex = (days + 19) % 20;
        string name = TzolkinNames[nameIndex];
        return $"{number} {name}";
    }

    private static string ToHaabPlaceholder(DateTime date)
    {
        return $"Haab-{date.DayOfYear % 365}";
    }
}
