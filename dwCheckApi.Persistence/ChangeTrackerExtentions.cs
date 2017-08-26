using System;
using dwCheckApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dwCheckApi.Persistence
{
    public static class ChangeTrackerExtentions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is BaseAuditClass baseAudit)) continue;
                
                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        baseAudit.Created = now;
                        baseAudit.Modified = now;
                        break;

                    case EntityState.Added:
                        baseAudit.Created = now;
                        break;
                }
            }
        }
    }
}