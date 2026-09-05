using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateDoctorWorkingDaysDto
    {
        public DayOfWeek FromDay { get; set; }
        public DayOfWeek ToDay { get; set; }
    }
}
