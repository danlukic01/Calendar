using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TattslottoRulesController : ControllerBase
    {
        private static readonly Dictionary<int, int> DateMatrix = new()
        {
            {1, 1}, {2, 2}, {3, 3}, {4, 4}, {5, 5}, {6, 6}, {7, 7}, {8, 8}, {9, 9},
            {10, 10}, {11, 11}, {12, 12}, {13, 13},
            {14, 1}, {15, 2}, {16, 3}, {17, 4}, {18, 5},
            {19, 6}, {20, 7}, {21, 8}, {22, 9}, {23, 10}, {24, 11}, {25, 12}, {26, 13},
            {27, 1}, {28, 2}, {29, 3}, {30, 4}, {31, 5}
        };

        [HttpGet]
        public ActionResult<object> Get([FromQuery] int day, [FromQuery] int month)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12)
            {
                return BadRequest("Invalid day or month");
            }

            int r16 = Rule16(day, month, DateMatrix);

            return Ok(new { rule16 = r16 });
        }


        private static int Rule16(int day, int month, Dictionary<int, int> matrix)
        {
            int sumDigits(int value) => value.ToString().Select(c => int.Parse(c.ToString())).Sum();

            return sumDigits(day) + sumDigits(matrix[day]) +
                   sumDigits(month) + sumDigits(matrix[month]);
        }
    }
}
