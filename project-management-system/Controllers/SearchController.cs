using Microsoft.AspNetCore.Mvc;
using project_management_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IDatabaseDriver _databaseDriver;

        public SearchController(IDatabaseDriver databaseDriver)
        {
            _databaseDriver = databaseDriver;
        }

        [Produces("application/json")]
        [HttpGet("searchAssignee")]
        public async Task<IActionResult> Search()
        {
            try
            {
                var term = HttpContext.Request.Query["term"].ToString();
                var matchingUsernames = await _databaseDriver.GetUserStartingWithTerm(term);
                return Ok(matchingUsernames);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
