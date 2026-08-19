using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ContactInfo
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? WhatsApp { get; set; }
        public string? Address { get; set; }
        public string? Hours { get; set; }        // مثلاً "24 ساعة" أو "من 9 ص - 10 م"
        public string? MapUrl { get; set; }        // رابط Google Maps للموقع

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
