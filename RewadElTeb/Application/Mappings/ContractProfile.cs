using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractDto, Contract>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.Ignore());

            CreateMap<UpdateContractDto, Contract>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Contract, ContractDto>();
        }
    }
}