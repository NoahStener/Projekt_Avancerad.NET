using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_Avancerad.NET.Data;

namespace Projekt_Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class HistoryController : ControllerBase
    {
        private readonly ProjektDbContext _projektDbContext;

        public HistoryController(ProjektDbContext projektDbContext)
        {
            _projektDbContext = projektDbContext;
        }

        [HttpGet("Appointments/history")]
        public async Task<ActionResult> GetAppointmentHistory()
        {
            try
            {
                var histories = await _projektDbContext.AppointmentHistories
                .OrderByDescending(h => h.ChangedAt)
                .ToListAsync();

                if(histories == null)
                {
                    return NotFound("No history was found");
                }

                return Ok(histories);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not post data from database..");

            }
        }
    }
}
