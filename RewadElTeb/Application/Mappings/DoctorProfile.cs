using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            // Create
            CreateMap<CreateDoctorDto, Doctor>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.Ignore());

            // Get Doctor
            CreateMap<Doctor, DoctorDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString())
                )
                .ForMember(
                    dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name)
                );

            CreateMap<UpdateDoctorDto, Doctor>()
          .ForMember(
              dest => dest.ImageUrl,
              opt => opt.Ignore());
        }
    }
}