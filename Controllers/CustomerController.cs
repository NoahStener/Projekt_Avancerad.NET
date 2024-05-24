using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Projekt_Avancerad.NET.Dto;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            _customerRepository = customerRepository;
        }

        //Funkar
        [HttpPost("Book Appointment/{custId:int}")]
        public async Task <ActionResult<Appointment>> BookAppointment(int custId, Appointment appointment)
        {
            try
            {
                if (!await _customerRepository.CustomerExists(custId))
                {
                    return NotFound("Customer not found");
                }

                var newAppointment = await _customerRepository.BookAppointment(custId, appointment);
                if (newAppointment == null)
                {
                    return BadRequest("Unable to book appointment.");
                }
                return CreatedAtAction(nameof(BookAppointment), new { custId, appointmentId = newAppointment.AppointmentID }, newAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //Funkar
        [HttpDelete("Cancel Appointment/{custId:int}/{appointmentId:int}")]
        public async Task<ActionResult<Appointment>> CancelAppointment(int custId, int appointmentId)
        {
            try
            {
                if(!await _customerRepository.CustomerExists(custId))
                {
                    return NotFound("Customer not found");
                }

                var appointment = await _customerRepository.CancelAppointment(custId, appointmentId);
                if(appointment == null)
                {
                    return NotFound("Not found or already canceled");
                }
                return Ok($"Appointment {appointmentId} canceled succesfully ");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //Funkar
        [HttpPut("Update Appointment/{custId:int}/{appointmentId:int}")]
        public async Task<ActionResult> UpdateAppointment(int custId, int appointmentId, Appointment updateAppointment)
        {
            try
            {
                if (!await _customerRepository.CustomerExists(custId))
                {
                    return NotFound("No customer found");
                }
                if (updateAppointment == null || updateAppointment.AppointmentID != appointmentId)
                {
                    return BadRequest();
                }
                
                var appointment = await _customerRepository.UpdateAppointment(custId, updateAppointment);
                if(appointment == null)
                {
                    return NotFound("CustomerID or AppointmentID didnt match");
                }

                return Ok(appointment);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        


    }
}
