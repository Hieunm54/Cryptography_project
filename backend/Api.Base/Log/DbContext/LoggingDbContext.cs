//using Api.Base.Log;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Microsoft.EntityFrameworkCore
//{
//    public class ActionAuditEntityConfiguration : IEntityTypeConfiguration<ActionAudit>
//    {
//        public void Configure(EntityTypeBuilder<ActionAudit> builder)
//        {
//            builder
//                .HasKey(x => x.ActionAuditId);
//            builder
//                .Property(x => x.UserName)
//                .IsRequired();
//        }
//    }

//    public class LoggingDbContext : DbContext
//    {
//        public DbSet<ActionAudit> ActionAudit { get; set; }

//        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            base.OnConfiguring(optionsBuilder);
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.ApplyConfiguration(new ActionAuditEntityConfiguration());
//        }
//    }
//}
