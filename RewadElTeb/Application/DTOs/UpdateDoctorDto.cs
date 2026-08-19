using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateDoctorDto
    {
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Specialization { get; set; } = null!;
        public string? Biography { get; set; }
        public int DepartmentId { get; set; }
        public DoctorStatus Status { get; set; }    
    }
}
