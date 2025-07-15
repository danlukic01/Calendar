using System;

namespace Calendar.Api.Models
{
    public class LottoEntry
    {
        public int Id { get; set; }
        public string LottoName { get; set; } = string.Empty;
        public DateTime DrawDate { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public int Powerball { get; set; }
        public int Supplement1 { get; set; }
        public int Supplement2 { get; set; }
        public int Supplement3 { get; set; }
    }
}
