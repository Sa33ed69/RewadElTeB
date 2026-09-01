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
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<CreateStatisticDto, Statistic>();

            CreateMap<UpdateStatisticDto, Statistic>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null));

            CreateMap<Statistic, StatisticDto>();
        }
    }
}
