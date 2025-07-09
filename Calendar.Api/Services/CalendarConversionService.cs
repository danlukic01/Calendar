using Calendar.Api.Models;

namespace Calendar.Api.Services;

public class CalendarConversionService
{
    public CalendarDate Convert(DateTime gregorian)
    {
        var dateOnly = gregorian.Date;
        return new CalendarDate
        {
            GregorianDate = dateOnly,
            JulianDate = ToJulianString(dateOnly),
            MayanLongCount = ToMayanLongCount(dateOnly),
            Tzolkin = ToTzolkin(dateOnly),
            Haab = ToHaab(dateOnly),
            CreatedAt = DateTime.UtcNow
        };
    }

    private static string ToJulianString(DateTime date)
    {
        // Convert to the Julian calendar date string (day/month/year)
        var julian = new System.Globalization.JulianCalendar();
        int year = julian.GetYear(date);
        int month = julian.GetMonth(date);
        int day = julian.GetDayOfMonth(date);
        return $"{day}/{month}/{year}";
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

    private static readonly string[] HaabMonths = new[]
    {
        "Pop", "Wo'", "Sip", "Sotz'", "Sek", "Xul", "Yaxk'in", "Mol",
        "Ch'en", "Yax", "Sak", "Keh", "Mak", "K'ank'in", "Muwan", "Pax",
        "K'ayab", "Kumk'u", "Wayeb"
    };

    private static string ToHaab(DateTime date)
    {
        int jdn = JulianDayNumber(date);
        int days = jdn - 584283;
        int count = (days + 348) % 365; // 0.0.0.0.0 = 8 Kumk'u
        int month = count / 20;
        int day = count % 20;
        string monthName = HaabMonths[month];
        return $"{day} {monthName}";
    }
}
