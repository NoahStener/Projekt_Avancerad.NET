using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Avancerad.NET.Dto;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Controllers
{
    //Gå igenom statuscodes meddelanden osv

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyAppointmentController : ControllerBase
    {
       
        private readonly ICompanyAppointmentRepository _appointmentRepository;
        private IMapper _mapper;
        public CompanyAppointmentController(ICompanyAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        //kanske justera utan att använda dto
        [HttpGet("appointments/week/{weekNumber}/year{year}")]
        public async Task<ActionResult> GetAppointmentsSpecificWeek(int weekNumber, int year)
        {

            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsForWeek(weekNumber, year);
                if (appointments == null || !appointments.Any())
                {
                    return NotFound("No appointments was found for this week");
                }
                var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
                return Ok(appointmentDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Could not retrieve data from database..");
            }
        }

        //Funkar (justera lite)
        [HttpGet("appointments/month/{month}/year{year}")]
        public async Task<ActionResult> GetAppointmentsSpecificMonth(int month, int year)
        {
            try
            {
                var appointments = await _appointmentRepository.GetAppointmentsForMonth(month, year);
                if (appointments == null || !appointments.Any())
                {
                    return NotFound("No appointments for this month");
                }
                var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
                return Ok(appointmentDtos);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }
        }

        //Funkar
        [HttpGet("All Appointments")]
        public async Task<ActionResult<Appointment>> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAll();
                if(appointments == null)
                {
                    return NotFound("No appointments were found");
                }
                return Ok(appointments);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }
        }


        //kanske justera utan att använda dto
        [HttpGet("Get Single/{appointmentId:int}")]
        public async Task<ActionResult<Appointment>> GetSingleAppointment(int appointmentId)            
        {
            try
            {
                var appointment = _mapper.Map<AppointmentDto>(await _appointmentRepository.GetSingle(appointmentId));
                if (appointment == null)
                {
                    return NotFound("Appointment not found");
                }
                var appointmentDto = _mapper.Map<AppointmentDto>(appointment);
                return Ok(appointmentDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }
        }



        [HttpPut("Update Appointment {appointmentId:int}")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(int appointmentId, Appointment updateAppointment)
        {

            try
            {
                if (appointmentId != updateAppointment.AppointmentID)
                {
                    return BadRequest("ID doesnt match");
                }

                var appointment = await _appointmentRepository.GetSingle(appointmentId);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {appointmentId} not found");
                }
                return await _appointmentRepository.Update(updateAppointment);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data..");
            }

        }


        [HttpPost("Add Appointment")]
        public async Task<ActionResult<Appointment>> AddAppointment(Appointment newAppointment)
        {
            try
            {
                if (newAppointment == null)
                {
                    return BadRequest("New appointment data is missing");
                }

                var addedAppointment = await _appointmentRepository.Add(newAppointment);
                if (addedAppointment == null)
                {
                    return BadRequest("Failed to add new customer");
                }
                //201 Created om customer läggs till
                return CreatedAtAction(nameof(GetSingleAppointment), new { appointmentId = addedAppointment.AppointmentID }, addedAppointment);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting data to database");
            }
        }

        [HttpDelete("Delete Appointment/{appointmentId:int}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentRepository.Delete(appointmentId);
                if (appointment == null)
                {
                    return NotFound("Appointment not found");
                }
                return Ok(appointment);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error removing data from database..");

            }

        }
    }
}
