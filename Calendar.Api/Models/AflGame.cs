using System;

namespace Calendar.Api.Models
{
    public class AflGame
    {
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Round { get; set; }
    }
}
