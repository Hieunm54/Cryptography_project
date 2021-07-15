using Api.Base.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore
{
    public class ActionAuditEntityConfiguration : IEntityTypeConfiguration<ActionAudit>
    {
        public void Configure(EntityTypeBuilder<ActionAudit> builder)
        {
            builder
                .HasKey(x => x.ActionAuditId);
            builder
                .Property(x => x.UserName)
                .IsRequired();

            builder.HasIndex(x => x.Action);
            builder.HasIndex(x => x.Controller);
            builder.HasIndex(x => x.BeginAuditTime);
            builder.HasIndex(x => x.LogLevel);
            builder.HasIndex(x => x.LogType);
        }
    }

    public class LoggingDbContext : DbContext
    {
        public DbSet<ActionAudit> ActionAudit { get; set; }
        public DbSet<MapLogging> MapLogging { get; set; }
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ActionAuditEntityConfiguration());
        }
    }
}
