
using System.Data.Entity.ModelConfiguration;
namespace Ataw.Framework.Core
{
    public class TreeCodeTableEntityMap : EntityTypeConfiguration<TreeCodeTableEntity>
    {
        public TreeCodeTableEntityMap()
        {
            this.HasKey(t => t.CODE_VALUE);
            this.ToTable("CD_SX");
            this.Property(t => t.CODE_DEL).HasColumnName("CODE_DEL");
            this.Property(t => t.CODE_NAME).HasColumnName("CODE_NAME");
            this.Property(t => t.CODE_PY).HasColumnName("CODE_PY");
            this.Property(t => t.CODE_SORT).HasColumnName("CODE_SORT");
            this.Property(t => t.CODE_DEL).HasColumnName("CODE_DEL");
        }
    }
}
