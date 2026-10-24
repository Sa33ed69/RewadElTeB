using Domain.Enums;

namespace Domain.Entities
{
    public class HomeAppointment
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = null!;

        public string PatientPhone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public HomeServiceType ServiceType { get; set; }

        public string CaseDescription { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}