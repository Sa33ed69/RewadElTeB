using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateHomeAppointmentDto
    {
        public string PatientName { get; set; } = null!;

        public string PatientPhone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public HomeServiceType ServiceType { get; set; }

        public string CaseDescription { get; set; } = null!;
    }
}
