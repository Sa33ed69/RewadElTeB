using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = null!;

        public string PatientPhone { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
