using Microsoft.Extensions.Logging.Abstractions;
using Projekt_Avancerad.NET.Dto;
using ProjektModels;

namespace Projekt_Avancerad.NET.Interfaces
{
    //Interface för kunderna, boka möte, avboka och updatera
    public interface ICustomerAppointmentRepository
    {
        Task<Appointment> BookAppointment(int custId, Appointment appointment);
        Task<Appointment> CancelAppointment(int custId, int appointmentId);
        Task<Appointment> UpdateAppointment(int custId, Appointment appointment);
        Task<bool> CustomerExists(int custId);
        Task<Appointment> GetAppointment(int custId, int appointmentId);

        public Task SaveAppointmentHistory(int appointmentId, int changedByCustomerId, string changeType, Appointment newValue, Appointment oldValue = null);


    }
}
