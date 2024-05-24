using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projekt_Avancerad.NET.Data;
using Projekt_Avancerad.NET.Dto;
using Projekt_Avancerad.NET.Helper;
using Projekt_Avancerad.NET.Interfaces;
using ProjektModels;

namespace Projekt_Avancerad.NET.Repository
{
    public class CustomerRepository : ICustomer
    {

        private ProjektDbContext _projektDbContext;
        private readonly IMapper _mapper;
        public CustomerRepository(ProjektDbContext projektDbContext, IMapper mapper)
        {
            _projektDbContext = projektDbContext;
            _mapper = mapper;
        }

        //Funkar
        public async Task<Appointment> BookAppointment(int customerId, Appointment appointment)
        {
            var customer = await _projektDbContext.Customers
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerID == customerId);

            if (customer != null)
            {
                var newAppointment = new Appointment
                {
                    CustomerID = customerId,
                    Title = appointment.Title,
                    Description = appointment.Description,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,

                };

                customer.Appointments.Add(newAppointment);
                await _projektDbContext.SaveChangesAsync();

                await SaveAppointmentHistory(newAppointment.AppointmentID, customerId, "Added", newAppointment);

                return newAppointment;
            }
            return null;

        }

        //Funkar
        public async Task<Appointment> CancelAppointment(int custId, int appointmentId)
        {
            var customer = await _projektDbContext.Customers
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerID == custId);

            if (customer != null)
            {
                var appointmentToCancel = customer.Appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
                if (appointmentToCancel != null)
                {
                    var oldValue = new Appointment
                    {
                        Title = appointmentToCancel.Title,
                        Description = appointmentToCancel.Description,
                        StartDate = appointmentToCancel.StartDate,
                        EndDate = appointmentToCancel.EndDate,
                    };

                    _projektDbContext.Appointments.Remove(appointmentToCancel);
                    await _projektDbContext.SaveChangesAsync();

                    await SaveAppointmentHistory(appointmentId, custId, "Deleted", oldValue);

                    return appointmentToCancel;
                }
            }
            return null;

        }

        public async Task<bool> CustomerExists(int custId)
        {
            return await _projektDbContext.Customers.AnyAsync(c => c.CustomerID == custId);
        }

        //Funkar
        public async Task<Appointment> GetAppointment(int custId, int appointmentId)
        {
            return await _projektDbContext.Appointments
                 .Include(a => a.Customer)
                 .FirstOrDefaultAsync(a => a.CustomerID == custId && a.AppointmentID == appointmentId);
        }
        //Funkar
        public async Task<Appointment> UpdateAppointment(int custId, Appointment updateAppointment)
        {
            var customer = await _projektDbContext.Customers
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.CustomerID == custId);

            if (customer != null)
            {
                var appointment = customer.Appointments.FirstOrDefault(a => a.AppointmentID == updateAppointment.AppointmentID);

                if (appointment != null)
                {
                    var oldValue = new Appointment
                    {
                        Title = appointment.Title,
                        Description = appointment.Description,
                        StartDate = appointment.StartDate,
                        EndDate = appointment.EndDate,
                    };

                    appointment.Title = updateAppointment.Title;
                    appointment.Description = updateAppointment.Description;
                    appointment.StartDate = updateAppointment.StartDate;
                    appointment.EndDate = updateAppointment.EndDate;

                    await _projektDbContext.SaveChangesAsync();

                    await SaveAppointmentHistory(updateAppointment.AppointmentID, custId, "Updated", appointment, oldValue);

                    return appointment;

                }
            }
            return null;

        }


        
        public async Task SaveAppointmentHistory(int appointmentId, int changedByCustomerId, string changeType, Appointment newValue, Appointment oldValue = null)
        {
            var history = new AppointmentHistory
            {
                AppointmentId = appointmentId,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = changedByCustomerId.ToString(),
                ChangeType = changeType,
                NewValue = newValue != null ? $"{newValue.Title}, {newValue.Description}, {newValue.StartDate}, {newValue.EndDate}" : null,
                OldValue = oldValue != null ? $"{oldValue.Title}, {oldValue.Description}, {oldValue.StartDate}, {oldValue.EndDate}" : null

            };

            _projektDbContext.AppointmentHistories.Add(history);
            await _projektDbContext.SaveChangesAsync();

        }
    }
}
