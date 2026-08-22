using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Specialization { get; set; } = null!;
        public string? Biography { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string Status { get; set; } = null!;

    }
}
