using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.AuthService
{
    public class AuditLogCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AuditLogCleanupService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(
    CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var dbContext = scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                    var now = DateTime.UtcNow;

                    var startOfCurrentMonth = new DateTime(
                        now.Year,
                        now.Month,
                        1,
                        0, 0, 0,
                        DateTimeKind.Utc);

                    // حذف كل الـ Audit Logs من الشهور السابقة
                    var oldLogs = await dbContext.AuditLogs
                        .Where(x => x.CreatedAt < startOfCurrentMonth)
                        .ToListAsync(stoppingToken);

                    if (oldLogs.Any())
                    {
                        dbContext.AuditLogs.RemoveRange(oldLogs);

                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception)
                {
                    // ممكن نضيف Logging هنا بعدين
                }

                // فحص مرة كل 24 ساعة
                await Task.Delay(
                    TimeSpan.FromDays(1),
                    stoppingToken);
            }
        }
    }
}
