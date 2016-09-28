using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_WORKFLOW_TRANSFERMap : EntityTypeConfiguration<WF_WORKFLOW_TRANSFER>
    {
        public WF_WORKFLOW_TRANSFERMap()
        {
            // Primary Key
            this.HasKey(t => t.WT_ID);

            // Properties
            this.Property(t => t.WT_ID);
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WT_TITLE)
                .HasMaxLength(100);

            this.Property(t => t.WT_TO_USER)
                .HasMaxLength(64);

            this.Property(t => t.WT_FROM_USER)
                .HasMaxLength(64);

            this.Property(t => t.WT_WD_NAME)
                .HasMaxLength(50);

            this.Property(t => t.WT_CREATE_ID)
                .HasMaxLength(64);

            this.Property(t => t.WT_UPDATE_ID)
                .HasMaxLength(64);

            this.Property(t => t.WT_WF_ID)
                .HasMaxLength(64);

            this.Property(t => t.WT_STEP_STATUS)
                .HasMaxLength(64);

            this.Property(t => t.WT_STEP_NAME)
                .HasMaxLength(64);

            this.Property(t => t.WT_WF_STATUS)
                .HasMaxLength(64);

            this.Property(t => t.WT_UNUSER3)
                .HasMaxLength(255);

            this.Property(t => t.WT_UNUSER4)
                .HasMaxLength(255);

            this.Property(t => t.WT_UNUSER5)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("WF_WORKFLOW_TRANSFER");
            this.Property(t => t.WT_ID).HasColumnName("WT_ID");
            this.Property(t => t.WT_TITLE).HasColumnName("WT_TITLE");
            this.Property(t => t.WT_TO_USER).HasColumnName("WT_TO_USER");
            this.Property(t => t.WT_FROM_USER).HasColumnName("WT_FROM_USER");
            this.Property(t => t.WT_TURN_TYPE).HasColumnName("WT_TURN_TYPE");
            this.Property(t => t.WT_WI_ID).HasColumnName("WT_WI_ID");
            this.Property(t => t.WT_WD_NAME).HasColumnName("WT_WD_NAME");
            this.Property(t => t.WT_END_TIME).HasColumnName("WT_END_TIME");
            this.Property(t => t.WT_IS_SUCCESS).HasColumnName("WT_IS_SUCCESS");
            this.Property(t => t.WT_DATA).HasColumnName("WT_DATA");
            this.Property(t => t.WT_NOTE).HasColumnName("WT_NOTE");
            this.Property(t => t.WT_CREATE_ID).HasColumnName("WT_CREATE_ID");
            this.Property(t => t.WT_CREATE_DATE).HasColumnName("WT_CREATE_DATE");
            this.Property(t => t.WT_UPDATE_ID).HasColumnName("WT_UPDATE_ID");
            this.Property(t => t.WT_UPDATE_DATE).HasColumnName("WT_UPDATE_DATE");
            this.Property(t => t.WT_WF_ID).HasColumnName("WT_WF_ID");
            this.Property(t => t.WT_IS_APPLY_WF).HasColumnName("WT_IS_APPLY_WF");
            this.Property(t => t.WT_STEP_STATUS).HasColumnName("WT_STEP_STATUS");
            this.Property(t => t.WT_STEP_NAME).HasColumnName("WT_STEP_NAME");
            this.Property(t => t.WT_WF_IS_END).HasColumnName("WT_WF_IS_END");
            this.Property(t => t.WT_WF_STATUS).HasColumnName("WT_WF_STATUS");
            this.Property(t => t.WT_UNUSER1).HasColumnName("WT_UNUSER1");
            this.Property(t => t.WT_UNUSER2).HasColumnName("WT_UNUSER2");
            this.Property(t => t.WT_UNUSER3).HasColumnName("WT_UNUSER3");
            this.Property(t => t.WT_UNUSER4).HasColumnName("WT_UNUSER4");
            this.Property(t => t.WT_UNUSER5).HasColumnName("WT_UNUSER5");
        }
    }
}
