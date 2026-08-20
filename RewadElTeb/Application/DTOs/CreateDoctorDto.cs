using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateDoctorDto
    {
        [Required]
        public string FullName { get; set; } = null!;
        public IFormFile? Image { get; set; }

        public string Specialization { get; set; } = null!;
        public string? Biography { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
