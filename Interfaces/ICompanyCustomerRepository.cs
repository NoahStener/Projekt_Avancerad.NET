using ProjektModels;

namespace Projekt_Avancerad.NET.Interfaces
{
    public interface ICompanyCustomerRepository : ICompany<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisWeek();
        Task<bool> CustomerExists(int custId);

        Task<Customer> GetCustomerDetails(int custId);
        //Hur många bokningar en kund har specifik vecka (ex antal timmar vecka 4)
        Task<int> GetCustomerAppointmentHoursForWeek(int custId, int weekNumber, int year);
    }
}
