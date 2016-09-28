using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_APPROVE_HISTORYMap : EntityTypeConfiguration<WF_APPROVE_HISTORY>
    {
        public WF_APPROVE_HISTORYMap()
        {
            // Primary Key
            this.HasKey(t => t.AH_ID);

            // Properties
            this.Property(t => t.AH_ID);
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AH_STEP_NAME)
                .HasMaxLength(50);

            this.Property(t => t.AH_STEP_DISPLAY_NAME)
                .HasMaxLength(255);

            this.Property(t => t.AH_OPERATOR)
                .HasMaxLength(64);

            this.Property(t => t.AH_CREATE_ID)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("WF_APPROVE_HISTORY");
            this.Property(t => t.AH_ID).HasColumnName("AH_ID");
            this.Property(t => t.AH_WORKFLOW_ID).HasColumnName("AH_WORKFLOW_ID");
            this.Property(t => t.AH_STEP_NAME).HasColumnName("AH_STEP_NAME");
            this.Property(t => t.AH_STEP_DISPLAY_NAME).HasColumnName("AH_STEP_DISPLAY_NAME");
            this.Property(t => t.AH_OPERATOR).HasColumnName("AH_OPERATOR");
            this.Property(t => t.AH_APPROVE).HasColumnName("AH_APPROVE");
            this.Property(t => t.AH_NOTE).HasColumnName("AH_NOTE");
            this.Property(t => t.AH_CREATE_ID).HasColumnName("AH_CREATE_ID");
            this.Property(t => t.AH_CREATE_DATE).HasColumnName("AH_CREATE_DATE");
        }
    }
}
