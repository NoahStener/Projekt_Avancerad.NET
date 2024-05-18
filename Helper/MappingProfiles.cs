using AutoMapper;
using Projekt_Avancerad.NET.Dto;
using ProjektModels;

namespace Projekt_Avancerad.NET.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
