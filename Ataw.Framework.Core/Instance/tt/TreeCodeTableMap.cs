
using System.Data.Entity.ModelConfiguration;
namespace Ataw.Framework.Core
{
    public class TTEntityMap : EntityTypeConfiguration<TTableEntity>
    {
        public TTEntityMap()
        {
            this.HasKey(t => t.FID);
            this.ToTable("TREE_TEST");
        }
    }
}
