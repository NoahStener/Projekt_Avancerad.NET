using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Avancerad.NET.Dto;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Controllers
{
    //Gå igenom statuscodes, meddelanden osv

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyCustomerController : ControllerBase
    {
        private readonly ICompanyCustomerRepository _customerRepository;
        private IMapper _mapper;
        public CompanyCustomerController(ICompanyCustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        //Funkar, hämtar kund med all information. kanske inte behövs
        [HttpGet("Get Customer with details/{custId:int}")]
        public async Task<ActionResult<Customer>> GetCustomerDetails(int custId)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerDetails(custId);
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }
                return Ok(customer);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }
        }

        //Funkar , hämtar kund och mappar till DTO
        [HttpGet("Get Customer/{custId:int}")]
        public async Task<ActionResult<Customer>> GetSingleCustomer(int custId)
        {
            try
            {
                var customer = _mapper.Map<CustomerDto>(await _customerRepository.GetSingle(custId));
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }
                //var customerDto = _mapper.Map<CustomerDto>(customer);
                return Ok(customer);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }
        }

        //Funkar
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>>GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                var customersDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                return Ok(customersDtos);
            
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not retrieve data from database..");
            }

        }

        //Funkar
        [HttpPost("AddCustomer")]
        public async Task<ActionResult<Customer>> AddCustomer(Customer newCustomer)
        {
            try
            {
                if (newCustomer == null)
                {
                    return BadRequest("New customer data is missing");
                }

                var createdCustomer = await _customerRepository.Add(newCustomer);
                //201 Created om customer läggs till
                return CreatedAtAction(nameof(GetSingleCustomer), new { custId = createdCustomer.CustomerID }, createdCustomer);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Could not post data to database..");
            }
        }

        //Funkar
        [HttpDelete("DeleteCustomer/{custId:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int custId)
        {
            try
            {
                var customer = await _customerRepository.Delete(custId);
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Could not remove data from database");
            }
        }

        //Funkar
        [HttpPut("UpdateCustomer/{custId:int}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int custId, Customer customer)
        {
            try
            {
                if (custId != customer.CustomerID)
                {
                    return BadRequest("ID doesnt match");
                }

                var custToUpdate = await _customerRepository.GetSingle(custId);
                if (custToUpdate == null)
                {
                    return NotFound($"Customer with ID {custId} not found");
                }
                return await _customerRepository.Update(customer);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        //Funkar, implementera DTO ?
        [HttpGet("Customers with appointments current week")]
        public async Task<ActionResult<Customer>> GetCustomersWithAppointmentsThisWeek()
        {
            try
            {
                var customers = await _customerRepository.GetCustomersWithAppointmentsThisWeek();
                if (customers == null)
                {
                    return NotFound("No Customers with appointments this week.");
                }

                return Ok(customers);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Funkar
        [HttpGet("Customers/{custId}/appointments/week/{weekNumber}/year{year}/hours")]
        public async Task<ActionResult> GetCustomerAppointmentHoursForWeek(int custId, int weekNumber, int year)
        {
            try
            {
                if (!await _customerRepository.CustomerExists(custId))
                {
                    return NotFound($"Customer with ID {custId} not found");
                }

                var hours = await _customerRepository.GetCustomerAppointmentHoursForWeek(custId, weekNumber, year);
                return Ok($"Customer with ID {custId} has appointments for {hours} hours week {weekNumber}");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
