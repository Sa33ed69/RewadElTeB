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
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.Ignore()
                );

            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.Ignore()
                );

            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentWithDoctorsDto>();
        }
    }
}
