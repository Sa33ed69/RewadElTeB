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
    public class ContactInfoProfile :Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<ContactInfo, ContactInfoDto>();

            CreateMap<CreateContactInfoDto, ContactInfo>();

            CreateMap<UpdateContactInfoDto, ContactInfo>();
        }
    }
}
