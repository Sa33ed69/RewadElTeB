using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateAppointmentDto
    {
        public string PatientName { get; set; } = null!;
        public string PatientPhone { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
    }
}
