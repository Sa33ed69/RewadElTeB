using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateStaffDto
    {
        public string Name { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }
        public int SortOrder { get; set; }
    }
}
