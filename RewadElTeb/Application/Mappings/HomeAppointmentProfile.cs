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
    public class HomeAppointmentProfile :Profile
    {
        public HomeAppointmentProfile()
        {
            CreateMap<CreateHomeAppointmentDto, HomeAppointment>();

            CreateMap<HomeAppointment, HomeAppointmentResponseDto>()
                .ForMember(
                    dest => dest.ServiceType,
                    opt => opt.MapFrom(src => src.ServiceType.ToString())
                );
                
        }
    }
}
