using System;

namespace Calendar.Api.Models
{
    public class LottoMatch
    {
        public int Id { get; set; }
        public string LottoName { get; set; } = string.Empty;
        public DateTime DrawDate { get; set; }
        public string Rule { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}

