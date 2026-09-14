using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interceptors
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditLogInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public override InterceptionResult<int> SavingChanges(
         DbContextEventData eventData,
         InterceptionResult<int> result)
        {
            AddAuditLogs(eventData.Context);

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
                   DbContextEventData eventData,
                   InterceptionResult<int> result,
                   CancellationToken cancellationToken = default)
        {
            AddAuditLogs(eventData.Context);

            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
        private void AddAuditLogs(DbContext? context)
        {
            if (context == null)
                return;

            if (string.IsNullOrEmpty(_currentUserService.UserId))
                return;

            var entries = context.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is not AuditLog &&
                    e.State is EntityState.Added
                        or EntityState.Modified
                        or EntityState.Deleted)
                .ToList();

            if (!entries.Any())
                return;

            var auditLogs = new List<AuditLog>();

            foreach (var entry in entries)
            {
                var action = entry.State switch
                {
                    EntityState.Added => "Create",
                    EntityState.Modified => "Update",
                    EntityState.Deleted => "Delete",
                    _ => null
                };

                if (action == null)
                    continue;

                string? entityId = null;

                var idProperty = entry.Properties
                    .FirstOrDefault(p => p.Metadata.Name == "Id");

                if (idProperty != null)
                {
                    entityId = idProperty.CurrentValue?.ToString();

                    if (entry.State == EntityState.Deleted)
                    {
                        entityId = idProperty.OriginalValue?.ToString();
                    }
                }

                auditLogs.Add(new AuditLog
                {
                    UserId = _currentUserService.UserId!,
                    UserName = _currentUserService.UserName ?? "Unknown",
                    UserEmail = _currentUserService.UserEmail ?? "Unknown",

                    Action = action,
                    EntityName = entry.Metadata.ClrType.Name,
                    EntityId = entityId,

                    CreatedAt = DateTime.UtcNow
                });
            }

            if (auditLogs.Any())
            {
                context.Set<AuditLog>().AddRange(auditLogs);
            }
        }
    }
}
