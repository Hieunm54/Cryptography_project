//using APITemplate.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace APITemplate.Infrastructure.Configuration
//{
//    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseContext>
//    {
//        public ApplicationDatabaseContext CreateDbContext(string[] args)
//        {
//            var dbOptionBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>();
//            var config = new ConfigurationBuilder()
//                               .SetBasePath(System.IO.Directory.GetCurrentDirectory())
//                               .AddJsonFile("appsettings.json")
//                               .Build();
//            var connectionString = config.GetConnectionString("UserDb");
//            dbOptionBuilder.UseMySql(connectionString,
//                ServerVersion.AutoDetect(connectionString));
//            return new ApplicationDatabaseContext(dbOptionBuilder.Options);

//        }
//    }
//}
