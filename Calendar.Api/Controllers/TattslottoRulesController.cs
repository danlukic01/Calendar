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
            {1,1}, {2,2}, {3,3}, {4,4}, {5,5}, {6,6}, {7,7}, {8,8}, {9,9},
            {10,10}, {11,11}, {12,12}, {13,13}, {14,14}, {15,15}, {16,16}, {17,17}, {18,18},
            {19,19}, {20,20}, {21,22}, {22,22}, {23,23}, {24,24}, {25,25}, {26,26},
            {27,27}, {28,28}, {29,29}, {30,30}, {31,31}
        };

        [HttpGet]
        public ActionResult<object> Get([FromQuery] int day, [FromQuery] int month)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12)
            {
                return BadRequest("Invalid day or month");
            }


            int r16 = Rule16(day, month, DateMatrix);
            int r17 = Rule17(day, month, DateMatrix);
            int r18 = Rule18(r16, r17);

            return Ok(new { rule16 = r16, rule17 = r17, rule18 = r18 });
        }


        {
            return day + matrix[day] + month + matrix[month];
        }


        private static int Rule17(int day, int month, Dictionary<int, int> matrix)


        {
            var digits = day.ToString().Select(d => int.Parse(d.ToString()))
                .Concat(month.ToString().Select(m => int.Parse(m.ToString())));
            int sum = 0;
            foreach (var d in digits)
            {
                sum += matrix[d];
            }
            return sum;
        }


        private static int Rule18(int rule16, int rule17)
        {
            var digits = rule16.ToString().Select(c => int.Parse(c.ToString()));
            return rule17 + digits.Sum();

        }
    }
}
