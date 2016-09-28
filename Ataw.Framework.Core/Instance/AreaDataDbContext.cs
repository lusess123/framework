
using System.Data.Entity;
namespace Ataw.Framework.Core
{
    public class AreaDataDbContext : AtawDbContext
    {
        static AreaDataDbContext()
        {
            Database.SetInitializer<AreaDataDbContext>(null);
        }

        public AreaDataDbContext(string connStr)
            : base(connStr)
        {
        }

        public AreaDataDbContext()
            : base("Name=ATAW_WORKFLOWContext")
        {
        }
        public DbSet<TreeCodeTableEntity> TreeCodeTable { get; set; }

        // public DbSet<WF_DataTable> WF_DataTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TreeCodeTableEntityMap());
        }
    }
}
