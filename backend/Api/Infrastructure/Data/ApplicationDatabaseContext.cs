using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APITemplate.Domain.Entities.Interfaces;
using APITemplate.Domain.Entities;
using APITemplate.EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace APITemplate.Infrastructure.Data
{
    public partial class ApplicationDatabaseContext : DbContext
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly ILogger<ApplicationDatabaseContext> Logger;
        
        public DbSet<User> User { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<DataUser> DataUser { get; set; }
        public DbSet<BlockData> BlockData { get; set; }
        public DbSet<Exchange> Exchange { get; set; }
        public ApplicationDatabaseContext(
            DbContextOptions<ApplicationDatabaseContext> options,
            IHttpContextAccessor httpContext,
            ILogger<ApplicationDatabaseContext> logger
            ) : base(options)
        {
            Logger = logger;
            this.httpContext = httpContext;
        }

        public ApplicationDatabaseContext(
            DbContextOptions<ApplicationDatabaseContext> options
            ) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration());

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is IAuditedEntityBase && (
                  e.State == EntityState.Added
                  || e.State == EntityState.Modified));

            string modifiedOrCreatedBy = httpContext?.HttpContext?.User?.Identity?.Name ?? "Anonymous";

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((IAuditedEntityBase)entityEntry.Entity).CreatedBy = modifiedOrCreatedBy;
                }
                else
                {
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedDate).IsModified = false;
                    Entry((IAuditedEntityBase)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }
                ((IAuditedEntityBase)entityEntry.Entity).LastModifiedDate = DateTime.Now;
                ((IAuditedEntityBase)entityEntry.Entity).LastModifiedBy = modifiedOrCreatedBy;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
