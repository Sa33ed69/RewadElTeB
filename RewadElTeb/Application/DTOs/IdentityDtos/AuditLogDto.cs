using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.IdentityDtos
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string? EntityDisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
