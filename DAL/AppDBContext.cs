using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class AppDBContext : DbContext
    {

        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer("data source = DESKTOP-JL03MFD\\SQLEXPRESS; Initial Catalog = EFCoreAssignment; persist security info = True; user id = Shrikant; password = Qwert@1234");
            }
        }
    }
}
