using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Calendar.Api.Services;
using Calendar.Api.Models;

namespace Calendar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AflFixturesController : ControllerBase
{
    private readonly AflFixtureService _service;

    public AflFixturesController(AflFixtureService service)
    {
        _service = service;
    }

    [HttpGet("current-round")]
    public async Task<IEnumerable<AflGame>> GetCurrentRound()
    {
        return await _service.GetCurrentRoundAsync();
    }
}
