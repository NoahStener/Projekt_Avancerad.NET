using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Projekt_Avancerad.NET.Data;
using Projekt_Avancerad.NET.Helper;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Repository
{
    //Updatera med liknande funktion som i CustomerAppointment
    public class CompanyAppointmentRepository : ICompanyAppointmentRepository
    {
        private readonly ProjektDbContext _projektDbContext;

        public CompanyAppointmentRepository(ProjektDbContext projektDbContext)
        {
            _projektDbContext = projektDbContext;
            
        }

        public async Task<Appointment> Add(Appointment newEntity)
        {
            var appointment = await _projektDbContext.Appointments.AddAsync(newEntity);
            await _projektDbContext.SaveChangesAsync();

            await SaveAppointmentHistory(newEntity.AppointmentID, "Company", "Added", newEntity);

            await _projektDbContext.SaveChangesAsync();

            return appointment.Entity;
        }

       
        public async Task<Appointment> Delete(int appointmentId)
        {
            var appointment = await _projektDbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentId);
            if (appointment != null)
            {
                var oldValue = new Appointment
                {
                    Title = appointment.Title,
                    Description = appointment.Description,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate
                };

                _projektDbContext.Appointments.Remove(appointment);
                await _projektDbContext.SaveChangesAsync();

                await SaveAppointmentHistory(appointmentId, "Company", "Deleted", null, oldValue);

                return appointment;
            }
            return null;
        }

        
        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _projektDbContext.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsForMonth(int month, int year)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            return await _projektDbContext.Appointments
                .Where(a => a.StartDate >= firstDayOfMonth && a.EndDate <= lastDayOfMonth)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsForWeek(int weekNumber, int year)
        {
            var firstDayOfWeek = DateHelper.FirstDateOfWeekISO8601(year, weekNumber);
            var lastDayOfWeek = firstDayOfWeek.AddDays(6);

            return await _projektDbContext.Appointments
                .Where(a => a.StartDate >= firstDayOfWeek && a.EndDate <= lastDayOfWeek)
                .ToListAsync();
        }

        public async Task<Appointment> GetSingle(int appointmentId)
        {
            return await _projektDbContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentId);
        }

        public async Task SaveAppointmentHistory(int appointmentId, string changedBy, string changeType, Appointment newValue, Appointment oldValue = null)
        {
            var history = new AppointmentHistory
            {
                AppointmentId = appointmentId,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = "Company",
                ChangeType = changeType,
                NewValue = newValue != null ? $"{newValue.Title}, {newValue.Description}, {newValue.StartDate}, {newValue.EndDate}" : null,
                OldValue = oldValue != null ? $"{oldValue.Title}, {oldValue.Description}, {oldValue.StartDate}, {oldValue.EndDate}" : null

            };

            _projektDbContext.AppointmentHistories.Add(history);
            await _projektDbContext.SaveChangesAsync();


        }

        //Implementerat historik funktion 
        public async Task<Appointment> Update(Appointment entity)
        {
            var appointment = await _projektDbContext.Appointments
               .FirstOrDefaultAsync(a => a.AppointmentID == entity.AppointmentID);

            if (appointment != null)
            {
                var oldValue = new Appointment
                {
                    Title = appointment.Title,
                    Description = appointment.Description,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                };

                appointment.Title = entity.Title;
                appointment.Description = entity.Description;
                appointment.StartDate = entity.StartDate;
                appointment.EndDate = entity.EndDate;

                await _projektDbContext.SaveChangesAsync();

                await SaveAppointmentHistory(entity.AppointmentID, "Company", "Updated", appointment, oldValue);

                return appointment;

            }
            return null;
        }

        
    }
}
