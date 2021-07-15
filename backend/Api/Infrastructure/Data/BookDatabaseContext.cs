using APITemplate.EntityConfiguration;
using APITemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace APITemplate.Infrastructure.Data
{
    public partial class ApplicationDatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
       
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
        }
    }
}
