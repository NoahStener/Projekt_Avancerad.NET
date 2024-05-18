using ProjektModels;

namespace Projekt_Avancerad.NET.Interfaces
{
    public interface ICompanyAppointmentRepository : ICompany<Appointment>
    {
        
        //Bokningar specifik vecka
        Task<IEnumerable<Appointment>> GetAppointmentsForWeek(int weekNumber, int year);
        //Bokningar specifik månad
        Task<IEnumerable<Appointment>> GetAppointmentsForMonth(int month, int year);

        //Implementera funktion för att spara historik som finns i ICustomerAppointmentRepository
        public Task SaveAppointmentHistory(int appointmentId, string changedBy, string changeType, Appointment newValue, Appointment oldValue = null);
    }
}
