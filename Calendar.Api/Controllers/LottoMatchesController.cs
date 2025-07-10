using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Data;
using Calendar.Api.Models;
using System.Linq;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LottoMatchesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LottoMatchesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<LottoMatch> Create([FromBody] LottoMatch match)
        {
            _context.LottoMatches.Add(match);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = match.Id }, match);
        }

        [HttpGet("{id}")]
        public ActionResult<LottoMatch> Get(int id)
        {
            var match = _context.LottoMatches.Find(id);
            if (match == null) return NotFound();
            return match;
        }

        [HttpGet]
        public IEnumerable<LottoMatch> List(string? lottoName, DateTime? drawDate, bool? matched)
        {
            var query = _context.LottoMatches.AsQueryable();
            if (!string.IsNullOrEmpty(lottoName))
                query = query.Where(m => m.LottoName == lottoName);
            if (drawDate.HasValue)
                query = query.Where(m => m.DrawDate.Date == drawDate.Value.Date);
            if (matched.HasValue)
                query = query.Where(m => m.Matched == matched.Value);
            return query.ToList();
        }
    }
}

