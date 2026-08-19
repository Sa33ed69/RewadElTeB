using Application.DTOs;
using Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result> CreateAsync(CreateDoctorDto dto);
        Task<Result> UpdateAsync(int id, UpdateDoctorDto dto);

    }
}
