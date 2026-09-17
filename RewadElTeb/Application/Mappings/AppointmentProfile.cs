using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            
             CreateMap<Appointment, AppointmentDto>()
    .ForMember(
        dest => dest.DoctorName,
        opt => opt.MapFrom(src => src.Doctor.FullName)
    )
    .ForMember(
        dest => dest.DepartmentName,
        opt => opt.MapFrom(src => src.Doctor.Department.Name)
    );

            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
