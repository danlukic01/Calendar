using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Data;
using Calendar.Api.Models;
using System.Linq;
using System;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LottoEntriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LottoEntriesController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/lottoentries
        [HttpPost]
        public ActionResult<LottoEntry> Create([FromBody] LottoEntry entry)
        {
            _context.LottoEntries.Add(entry);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
        }

        // GET api/lottoentries/{id}
        [HttpGet("{id}")]
        public ActionResult<LottoEntry> Get(int id)
        {
            var entry = _context.LottoEntries.Find(id);
            if (entry == null) return NotFound();
            return entry;
        }

        // GET api/lottoentries
        [HttpGet]
        public IEnumerable<LottoEntry> List()
        {
            return _context.LottoEntries
                .OrderByDescending(e => e.DrawDate)
                .ToList();
        }

        [HttpGet("byDate")]
        public ActionResult<LottoEntry> GetByDate(string lottoName, DateTime drawDate)
        {
            var entry = _context.LottoEntries.FirstOrDefault(e => e.LottoName == lottoName && e.DrawDate.Date == drawDate.Date);
            if (entry == null) return NotFound();
            return entry;
        }
    }
}
