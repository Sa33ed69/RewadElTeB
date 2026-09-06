
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;            
        public string? Description { get; set; }                   
        public string? ImageUrl { get; set; }                 

    }
}
