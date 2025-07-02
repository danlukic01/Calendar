using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Data;
using Calendar.Api.Models;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalculationsController(AppDbContext context)
        {
            _context = context;
        }

        // POST api/calculations
        [HttpPost]
        public IActionResult CreateCalculation([FromBody] IntervalCalculation request)
        {
            // Placeholder implementation
            _context.IntervalCalculations.Add(request);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetResult), new { id = request.Id }, request);
        }

        // GET api/calculations/{id}/result
        [HttpGet("{id}/result")]
        public ActionResult<IntervalCalculationResult> GetResult(int id)
        {
            var result = _context.IntervalCalculationResults.FirstOrDefault(r => r.IntervalCalculationId == id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
