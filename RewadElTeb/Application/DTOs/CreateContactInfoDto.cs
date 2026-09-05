using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateContactInfoDto
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? WhatsApp { get; set; }
        public string? Address { get; set; }
        public string? Hours { get; set; }
        public string? MapUrl { get; set; }
    }
}
