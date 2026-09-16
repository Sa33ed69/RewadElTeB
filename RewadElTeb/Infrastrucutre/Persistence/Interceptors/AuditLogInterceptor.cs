using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditLogInterceptor(
            ICurrentUserService currentUserService)
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

                // Entity Id
                string? entityId = null;

                var idProperty = entry.Properties
                    .FirstOrDefault(p =>
                        p.Metadata.Name == "Id");

                if (idProperty != null)
                {
                    entityId = entry.State == EntityState.Deleted
                        ? idProperty.OriginalValue?.ToString()
                        : idProperty.CurrentValue?.ToString();
                }

                // Entity Display Name
                string? entityDisplayName = GetEntityDisplayName(
                    entry,
                    context);

                auditLogs.Add(new AuditLog
                {
                    UserId = _currentUserService.UserId!,
                    UserName = _currentUserService.UserName ?? "Unknown",
                    UserEmail = _currentUserService.UserEmail ?? "Unknown",

                    Action = action,

                    EntityName = entry.Metadata.ClrType.Name,

                    EntityId = entityId,

                    EntityDisplayName = entityDisplayName,

                    CreatedAt = DateTime.UtcNow
                });
            }

            if (auditLogs.Any())
            {
                context.Set<AuditLog>()
                    .AddRange(auditLogs);
            }
        }

        private string? GetEntityDisplayName(
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry,
            DbContext context)
        {
            // Create / Update
            if (entry.State != EntityState.Deleted)
            {
                var fullNameProperty = entry.Properties
                    .FirstOrDefault(p =>
                        p.Metadata.Name == "FullName");

                if (fullNameProperty?.CurrentValue != null)
                {
                    return fullNameProperty.CurrentValue.ToString();
                }

                var nameProperty = entry.Properties
                    .FirstOrDefault(p =>
                        p.Metadata.Name == "Name");

                if (nameProperty?.CurrentValue != null)
                {
                    return nameProperty.CurrentValue.ToString();
                }
            }

            // Delete
            var originalFullNameProperty = entry.Properties
                .FirstOrDefault(p =>
                    p.Metadata.Name == "FullName");

            if (originalFullNameProperty?.OriginalValue != null)
            {
                return originalFullNameProperty
                    .OriginalValue
                    .ToString();
            }

            var originalNameProperty = entry.Properties
                .FirstOrDefault(p =>
                    p.Metadata.Name == "Name");

            if (originalNameProperty?.OriginalValue != null)
            {
                return originalNameProperty
                    .OriginalValue
                    .ToString();
            }

            return null;
        }
    }
}