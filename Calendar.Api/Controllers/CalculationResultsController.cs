using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Data;
using Calendar.Api.Models;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalculationResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/calculationresults
        [HttpGet]
        public IEnumerable<IntervalCalculationResult> GetResults()
        {
            return _context.IntervalCalculationResults.ToList();
        }
    }
}
