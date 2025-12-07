using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;

namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.AuditTrail
{
    public class AuditHelper
    {
        readonly AnaPreventionContext Db;

        public AuditHelper(AnaPreventionContext db)
        {
            Db = db;
        }

        public void AddAuditLogs(Guid userId, EntityEntry entry, AuditType auditType = AuditType.Update)
        {
            Db.ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = new();
            if (auditType != AuditType.Create)
            {

                if ((entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged ||
                    entry.State == EntityState.Added) == false)
                {
                    var auditEntry = new AuditEntry(entry, userId);
                    auditEntries.Add(auditEntry);
                }

            }
            else
            {

                if (entry.Entity is not Audit)
                {
                    var auditEntry = new AuditEntry(entry, userId, auditType);
                    auditEntries.Add(auditEntry);
                }                

            }

            if (auditEntries.Count != 0)
            {
                var logs = auditEntries.Select(x => x.ToAudit());
                Db.AddRange(logs);
            }
        }
    }
}
