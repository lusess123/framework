
using System.Data.Entity;
namespace Ataw.Framework.Core
{
    public class TTDbContext : AtawDbContext
    {
        static TTDbContext()
        {
            Database.SetInitializer<AreaDataDbContext>(null);
        }

        public TTDbContext(string connStr)
            : base(connStr)
        {
        }

        public TTDbContext()
            : base("Name=ATAW_WORKFLOWContext")
        {
        }
        public DbSet<TTableEntity> TreeCodeTable { get; set; }

        // public DbSet<WF_DataTable> WF_DataTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TTEntityMap());
        }
    }
}
