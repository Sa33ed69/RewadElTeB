using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Statistic
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;      
        public string Value { get; set; } = null!;      

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
