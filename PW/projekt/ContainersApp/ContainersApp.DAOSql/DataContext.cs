using Microsoft.EntityFrameworkCore;

namespace ContainersApp.DAOSql
{
    public class DataContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=containers.db");
        }

        public virtual DbSet<BO.Container> containers { get; set; }
        public virtual DbSet<BO.Producer> producers { get; set; }
    }
}