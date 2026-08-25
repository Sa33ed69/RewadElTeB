using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<CreateStaffDto, Staff>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.Ignore());

            CreateMap<UpdateStaffDto, Staff>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.Ignore());

            CreateMap<Staff, StaffDto>();
        }
    }
}