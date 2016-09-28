using System.Data.Entity.ModelConfiguration;

namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_WORKFLOW_DEFMap : EntityTypeConfiguration<WF_WORKFLOW_DEF>
    {
        public WF_WORKFLOW_DEFMap()
        {
            // Primary Key
            this.HasKey(t => t.WD_SHORT_NAME);

            // Properties
            this.Property(t => t.WD_SHORT_NAME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.WD_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.WD_DESCRIPTION)
                .HasMaxLength(255);

            this.Property(t => t.WD_VERSION)
                .HasMaxLength(20);

            this.Property(t => t.WD_VERSION_DESC)
                .HasMaxLength(255);

            this.Property(t => t.WD_CREATE_ID)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.WD_UPDATE_ID)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.WD_UNUSED3)
                .HasMaxLength(255);

            this.Property(t => t.WD_UNUSED4)
                .HasMaxLength(255);

            this.Property(t => t.WD_UNUSED5)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("WF_WORKFLOW_DEF");
            this.Property(t => t.WD_ID).HasColumnName("WD_ID");
            this.Property(t => t.WD_SHORT_NAME).HasColumnName("WD_SHORT_NAME");
            this.Property(t => t.WD_NAME).HasColumnName("WD_NAME");
            this.Property(t => t.WD_DESCRIPTION).HasColumnName("WD_DESCRIPTION");
            this.Property(t => t.WD_CONTENT).HasColumnName("WD_CONTENT");
            this.Property(t => t.WD_IS_USED).HasColumnName("WD_IS_USED");
            this.Property(t => t.WD_VERSION).HasColumnName("WD_VERSION");
            this.Property(t => t.WD_VERSION_DESC).HasColumnName("WD_VERSION_DESC");
            this.Property(t => t.WD_CREATE_ID).HasColumnName("WD_CREATE_ID");
            this.Property(t => t.WD_CREATE_DATE).HasColumnName("WD_CREATE_DATE");
            this.Property(t => t.WD_UPDATE_ID).HasColumnName("WD_UPDATE_ID");
            this.Property(t => t.WD_UPDATE_DATE).HasColumnName("WD_UPDATE_DATE");
            this.Property(t => t.WD_UNUSED1).HasColumnName("WD_UNUSED1");
            this.Property(t => t.WD_UNUSED2).HasColumnName("WD_UNUSED2");
            this.Property(t => t.WD_UNUSED3).HasColumnName("WD_UNUSED3");
            this.Property(t => t.WD_UNUSED4).HasColumnName("WD_UNUSED4");
            this.Property(t => t.WD_UNUSED5).HasColumnName("WD_UNUSED5");
            this.Property(t => t.FControlUnitID).HasColumnName("FControlUnitID");
        }
    }
}
