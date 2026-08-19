using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string Specialization { get; set; } = null!;

        public string? Biography { get; set; }

        public DoctorStatus Status { get; set; } = DoctorStatus.Active;

        public ICollection<Appointment> Appointments { get; set; }
    = new List<Appointment>();
        public int DepartmentId { get; set; }

        public Department Department { get; set; } = null!;
       
    }
}
