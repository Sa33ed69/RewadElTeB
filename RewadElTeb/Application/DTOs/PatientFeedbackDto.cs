using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PatientFeedbackDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
