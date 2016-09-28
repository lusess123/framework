using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_STEP_INSTMap : EntityTypeConfiguration<WF_STEP_INST>
    {
        public WF_STEP_INSTMap()
        {
            // Primary Key
            this.HasKey(t => t.SI_ID);

            // Properties
            this.Property(t => t.SI_ID);
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SI_LAST_STEP)
                .HasMaxLength(50);

            this.Property(t => t.SI_LAST_STEP_NAME)
                .HasMaxLength(200);

            this.Property(t => t.SI_LAST_MANUAL)
                .HasMaxLength(50);

            this.Property(t => t.SI_LAST_MANUAL_NAME)
                .HasMaxLength(200);

            this.Property(t => t.SI_CURRENT_STEP)
                .HasMaxLength(50);

            this.Property(t => t.SI_CURRENT_STEP_NAME)
                .HasMaxLength(200);

            this.Property(t => t.SI_RECEIVE_ID)
                .HasMaxLength(64);

            this.Property(t => t.SI_SEND_ID)
                .HasMaxLength(64);

            this.Property(t => t.SI_PROCESS_ID)
                .HasMaxLength(64);

            this.Property(t => t.SI_NOTE)
                .HasMaxLength(255);

            this.Property(t => t.SI_UNUSED4)
                .HasMaxLength(255);

            this.Property(t => t.SI_UNUSED5)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("WF_STEP_INST");
            this.Property(t => t.SI_ID).HasColumnName("SI_ID");
            this.Property(t => t.SI_WI_ID).HasColumnName("SI_WI_ID");
            this.Property(t => t.SI_STATUS).HasColumnName("SI_STATUS");
            this.Property(t => t.SI_LAST_STEP).HasColumnName("SI_LAST_STEP");
            this.Property(t => t.SI_LAST_STEP_NAME).HasColumnName("SI_LAST_STEP_NAME");
            this.Property(t => t.SI_LAST_MANUAL).HasColumnName("SI_LAST_MANUAL");
            this.Property(t => t.SI_LAST_MANUAL_NAME).HasColumnName("SI_LAST_MANUAL_NAME");
            this.Property(t => t.SI_CURRENT_STEP).HasColumnName("SI_CURRENT_STEP");
            this.Property(t => t.SI_CURRENT_STEP_NAME).HasColumnName("SI_CURRENT_STEP_NAME");
            this.Property(t => t.SI_STEP_TYPE).HasColumnName("SI_STEP_TYPE");
            this.Property(t => t.SI_PRIORITY).HasColumnName("SI_PRIORITY");
            this.Property(t => t.SI_RECEIVE_ID).HasColumnName("SI_RECEIVE_ID");
            this.Property(t => t.SI_RECEIVE_DATE).HasColumnName("SI_RECEIVE_DATE");
            this.Property(t => t.SI_SEND_ID).HasColumnName("SI_SEND_ID");
            this.Property(t => t.SI_SEND_DATE).HasColumnName("SI_SEND_DATE");
            this.Property(t => t.SI_PROCESS_ID).HasColumnName("SI_PROCESS_ID");
            this.Property(t => t.SI_PROCESS_DATE).HasColumnName("SI_PROCESS_DATE");
            this.Property(t => t.SI_START_DATE).HasColumnName("SI_START_DATE");
            this.Property(t => t.SI_END_DATE).HasColumnName("SI_END_DATE");
            this.Property(t => t.SI_TIME_SPAN).HasColumnName("SI_TIME_SPAN");
            this.Property(t => t.SI_IS_TIMEOUT).HasColumnName("SI_IS_TIMEOUT");
            this.Property(t => t.SI_TIMEOUT_DATE).HasColumnName("SI_TIMEOUT_DATE");
            this.Property(t => t.SI_NOTE).HasColumnName("SI_NOTE");
            this.Property(t => t.SI_UNUSED1).HasColumnName("SI_UNUSED1");
            this.Property(t => t.SI_UNUSED2).HasColumnName("SI_UNUSED2");
            this.Property(t => t.SI_UNUSED3).HasColumnName("SI_UNUSED3");
            this.Property(t => t.SI_UNUSED4).HasColumnName("SI_UNUSED4");
            this.Property(t => t.SI_UNUSED5).HasColumnName("SI_UNUSED5");
            this.Property(t => t.SI_FLOW_TYPE).HasColumnName("SI_FLOW_TYPE");
            this.Property(t => t.SI_VALID_FLAG).HasColumnName("SI_VALID_FLAG");
            this.Property(t => t.SI_INDEX).HasColumnName("SI_INDEX");
        }
    }
}
