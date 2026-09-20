using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class HomeAppointmentResponseDto
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = null!;

        public string PatientPhone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string ServiceType { get; set; }

        public string CaseDescription { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
