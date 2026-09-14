using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;

        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string? EntityId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
