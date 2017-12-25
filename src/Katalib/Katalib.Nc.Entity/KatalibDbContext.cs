using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Katalib.Nc.Entity
{
    /// <summary>
    ///
    /// </summary>
    public abstract class KatalibDbContext : DbContext
    {

        public KatalibDbContext()
        {
        }
        
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                // IAuditableEntity
                IAuditableEntity auditableEntity = entry.Entity as IAuditableEntity;
                if (auditableEntity != null) ProcAuditableEntity(entry, auditableEntity);

                // ISaveEntity
                ISaveEntity saveEntity = entry.Entity as ISaveEntity;
                if (saveEntity != null) ProcSaveEntity(saveEntity);
            }

            // 削除対象のEntityで、IDeleteEntityを実装しているオブジェクトを見つける
            var deleteEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IDeleteEntity
                    && (x.State == EntityState.Deleted));
            foreach (var entry in deleteEntries)
            {
                // IDeleteEntity
                IDeleteEntity deleteEntity = entry.Entity as IDeleteEntity;
                if (deleteEntity != null) ProcDeleteEntity(deleteEntity);
            }

            return base.SaveChanges();
        }

        private void ProcAuditableEntity(EntityEntry entry, IAuditableEntity auditableEntity)
        {
            string identityName = "SYS";//Thread.CurrentPrincipal.Identity.Name;
            DateTime now = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                auditableEntity.CreatedBy = identityName;
                auditableEntity.CreatedDate = now;
            }
            else
            {
                base.Entry(auditableEntity).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(auditableEntity).Property(x => x.CreatedDate).IsModified = false;
            }

            auditableEntity.UpdatedBy = identityName;
            auditableEntity.UpdatedDate = now;
        }

        private void ProcSaveEntity(ISaveEntity entity)
        {
            entity.OnSave();
        }

        private void ProcDeleteEntity(IDeleteEntity entity)
        {
            entity.OnDelete(this);
        }

    }
}