using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class AssetDbContext : DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options)
        {

        }

        //defining DbSet properties for the created entites 
        public DbSet<Users> Users { get; set; }
        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }

        public DbSet<AssetTypes> AssetTypes { get; set; }
        public DbSet<AssetMaster> AssetMaster { get; set; }
        public DbSet<AssetAllocation> AssetAllocation { get; set; }

        public DbSet<Leave> Leave { get; set; }

        public DbSet<LeaveRecord> LeaveRecord { get; set; }

        public DbSet<SalaryMaster> SalaryMaster { get; set; }

        public DbSet<SalarySheet> SalarySheet { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Con");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
