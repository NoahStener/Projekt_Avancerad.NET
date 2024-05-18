using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekt_Avancerad.NET.Data;
using Projekt_Avancerad.NET.Helper;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Repository
{

    //Allt funkar som det ska i stort sett
    //Gå igenom igen och kolla efter förbättringar
    public class CompanyCustomerRepository : ICompanyCustomerRepository
    {
        private readonly ProjektDbContext _projektDbContext;
        public CompanyCustomerRepository(ProjektDbContext projektDbContext)
        {
            _projektDbContext = projektDbContext;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var customer = await _projektDbContext.Customers.AddAsync(newEntity);
            await _projektDbContext.SaveChangesAsync();
            return customer.Entity;
        }

        public async Task<bool> CustomerExists(int custId)
        {
            return await _projektDbContext.Customers.AnyAsync(c => c.CustomerID == custId);
        }

        public async Task<Customer> Delete(int custId)
        {
            var custDelete = await _projektDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == custId);
            if (custDelete != null)
            {
                _projektDbContext.Customers.Remove(custDelete);
                await _projektDbContext.SaveChangesAsync();
                return custDelete;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _projektDbContext.Customers.ToListAsync();
        }

        public async Task<int> GetCustomerAppointmentHoursForWeek(int custId, int weekNumber, int year)
        {
            var firstDayOfWeek = DateHelper.FirstDateOfWeekISO8601(year, weekNumber);
            var lastDayOfWeek = firstDayOfWeek.AddDays(6);

            return await _projektDbContext.Appointments
                .CountAsync(a => a.CustomerID == custId && a.StartDate >= firstDayOfWeek && a.EndDate <= lastDayOfWeek);
        }

        public async Task<Customer> GetCustomerDetails(int custId)
        {
            var custWithAppointment = await _projektDbContext.Customers
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerID == custId);

            return custWithAppointment;
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisWeek()
        {
            var currentDate = DateTime.Now;
            var currentDayOfWeek = (int)currentDate.DayOfWeek;
            var startDateOfWeek = currentDate.AddDays(-currentDayOfWeek + (int)DayOfWeek.Monday);
            var endDateOfWeek = startDateOfWeek.AddDays(7).AddTicks(-1);

            var customers = await _projektDbContext.Customers
                .Include(c => c.Appointments)
                .Where(c => c.Appointments.Any(a => a.StartDate >= startDateOfWeek && a.EndDate <= endDateOfWeek))
                .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetSingle(int custId)
        {
            return await _projektDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == custId);
        }

        public async Task<Customer> Update(Customer entity)
        {
            var custUpdate = await _projektDbContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == entity.CustomerID);

            if (custUpdate != null)
            {
                custUpdate.FirstName = entity.FirstName;
                custUpdate.LastName = entity.LastName;
                custUpdate.Email = entity.Email;
                custUpdate.Address = entity.Address;
                custUpdate.Phone = entity.Phone;

                await _projektDbContext.SaveChangesAsync();
                return custUpdate;
            }
            return null;
        }
    }
}
