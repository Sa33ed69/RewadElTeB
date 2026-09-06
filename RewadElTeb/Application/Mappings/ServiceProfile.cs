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
    public class ServiceProfile :Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceDto, Service>();

            CreateMap<UpdateServiceDto, Service>();

            CreateMap<Service, ServiceDto>();
        }
    }
}
